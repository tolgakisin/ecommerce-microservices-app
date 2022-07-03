using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Orchestrator.Data.Common;
using Orchestrator.Saga.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ
{
    public class RabbitBus : IRabbitBus
    {
        private readonly IConfiguration _configuration;
        private IModel _channel;
        private readonly IRabbitMQBase _rabbitMQBase;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private const string exchangeName = "general-exchange";
        private static readonly object _lock = new();
        private readonly Context _context;

        public RabbitBus(IRabbitMQBase rabbitMQBase, IConfiguration configuration, Context context)
        {
            _rabbitMQBase = rabbitMQBase;
            _channel = OpenChannel();
            _configuration = configuration;

            _context = context;
        }

        public async Task<T> SendMessageAsync<T>(T message) where T : SagaModel
        {
            return await Task.Run(() =>
            {
                if (_channel == null || _channel.IsClosed)
                {
                    lock (_lock)
                    {
                        if (_channel == null || _channel.IsClosed)
                        {
                            _channel = OpenChannel();
                        }
                    }
                }

                var props = _channel.CreateBasicProperties();

                ReceiveMessage(props, message.IsSync);
                PublishMessage(message, props);

                if (!message.IsSync) return null;

                var isTaken = _respQueue.TryTake(out var responseMessage, TimeSpan.FromSeconds(30));

                return responseMessage != null ? JsonConvert.DeserializeObject<T>(responseMessage) : null;
            });

        }

        private void ReceiveMessage(IBasicProperties props, bool isSync)
        {
            //var eventsConfig = _configuration.GetSection("Events");
            //var events = eventsConfig.Get<List<string>>();

            var replyQueueName = _channel.QueueDeclare().QueueName;
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);
                var response = JsonConvert.DeserializeObject<SagaModel>(bodyJson);

                string nextEventName = string.Empty;
                if (response.IsFailed)
                {
                    var previousEvent = _context.GetPreviousEvent(response.EventName);
                    nextEventName = previousEvent?.Name; //Utils.GetPreviousEvent(events, response.EventName).EventName;
                }
                else
                {
                    var nextEvent = _context.GetNextEvent(response.EventName);
                    nextEventName = nextEvent?.Name; //Utils.GetNextEvent(events, response.EventName).EventName;
                }

                if (!string.IsNullOrEmpty(nextEventName))
                {
                    response.EventName = nextEventName;
                    PublishMessage(response, props);
                }
                else
                {
                    CloseChannel();

                    if (isSync && ea.BasicProperties.CorrelationId == correlationId)
                    {
                        _respQueue.Add(bodyJson);
                    }
                }
            };

            _channel.BasicConsume(consumer: consumer, queue: replyQueueName, autoAck: true);
        }

        private void PublishMessage(SagaModel model, IBasicProperties props = null)
        {
            _channel.QueueDeclare(queue: model.EventName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            _channel.ExchangeDeclare(exchangeName, "direct", false, false);
            _channel.QueueBind(queue: model.EventName, exchangeName, model.EventName);

            var serializedMessage = JsonConvert.SerializeObject(model);
            var body = System.Text.Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(exchange: exchangeName,
                                routingKey: model.EventName,
                                basicProperties: props,
                                body: body);
        }

        private IModel OpenChannel()
        {
            return _rabbitMQBase.CreateBus().CreateModel();
        }

        private void CloseChannel()
        {
            if (_channel.IsOpen)
                _channel.Close();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Common.Enums;
using Orchestrator.Data.Common;
using Orchestrator.Data.Entities;
using Orchestrator.Saga.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ
{
    public class RabbitBus : IRabbitBus
    {
        private IModel _channel;
        private readonly IRabbitMQBase _rabbitMQBase;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private const string exchangeName = "general-exchange";
        private static readonly object _lock = new();

        public RabbitBus(IRabbitMQBase rabbitMQBase)
        {
            _rabbitMQBase = rabbitMQBase;
            _channel = OpenChannel();
        }

        public async Task<object> SendMessageAsync(object messageObj, SagaModel sagaMessage, IApplicationBuilder app)
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

                ReceiveMessage(props, sagaMessage.IsSync, app);
                PublishMessage(messageObj, sagaMessage, props);

                if (!sagaMessage.IsSync) return null;

                var isTaken = _respQueue.TryTake(out var responseMessage, TimeSpan.FromSeconds(200));

                return responseMessage != null ? JsonConvert.DeserializeObject(responseMessage) : null;
            });

        }

        private void ReceiveMessage(IBasicProperties props, bool isSync, IApplicationBuilder app)
        {
            var replyQueueName = _channel.QueueDeclare().QueueName;
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetService<Context>();
                    var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);

                    var objMessage = JObject.Parse(bodyJson);
                    var sagaMessage = objMessage.ToObject<SagaModel>();

                    string nextEventName = string.Empty;
                    int nextEventId = 0;
                    if (sagaMessage.IsFailed)
                    {
                        _context.Save<EventLog>(new EventLog
                        {
                            EventId = sagaMessage.EventId,
                            Data = sagaMessage.Data,
                            ExecutionDate = DateTime.Now,
                            State = !sagaMessage.IsReverseStarted ? EventState.Failed : !sagaMessage.IsFinished ? EventState.ReverseStarted : EventState.ReverseFinished,
                            ErrorMessage = sagaMessage.ErrorMessage
                        });

                        if (!sagaMessage.IsReverseStarted)
                        {
                            sagaMessage.IsReverseStarted = true;
                            objMessage[nameof(sagaMessage.IsReverseStarted)] = true;
                        }

                        var previousEvent = _context.GetPreviousEvent(sagaMessage.EventName);
                        nextEventName = previousEvent?.Name; //Utils.GetPreviousEvent(events, response.EventName).EventName;
                        nextEventId = previousEvent?.Id ?? 0;
                    }
                    else
                    {
                        _context.Save<EventLog>(new EventLog
                        {
                            EventId = sagaMessage.EventId,
                            Data = sagaMessage.Data,
                            ExecutionDate = DateTime.Now,
                            State = EventState.Finished
                        });

                        var nextEvent = _context.GetNextEvent(sagaMessage.EventName);
                        nextEventName = nextEvent?.Name; //Utils.GetNextEvent(events, response.EventName).EventName;
                        nextEventId = nextEvent?.Id ?? 0;
                    }

                    if (!string.IsNullOrEmpty(nextEventName))
                    {
                        sagaMessage.EventName = nextEventName;
                        objMessage[nameof(sagaMessage.EventName)] = nextEventName;
                        sagaMessage.EventId = nextEventId;
                        objMessage[nameof(sagaMessage.EventId)] = nextEventId;

                        PublishMessage(objMessage, sagaMessage, props);

                        _context.Save<EventLog>(new EventLog
                        {
                            EventId = sagaMessage.EventId,
                            Data = sagaMessage.Data,
                            ExecutionDate = DateTime.Now,
                            State = sagaMessage.IsFailed ? !sagaMessage.IsFinished ? EventState.ReverseStarted : EventState.ReverseFinished : EventState.Started,
                            ErrorMessage = sagaMessage.ErrorMessage
                        });
                    }
                    else
                    {
                        CloseChannel();

                        if (isSync && ea.BasicProperties.CorrelationId == correlationId)
                        {
                            _respQueue.Add(bodyJson);
                        }
                    }
                }
            };

            _channel.BasicConsume(consumer: consumer, queue: replyQueueName, autoAck: true);
        }

        private void PublishMessage(object objMessage, SagaModel sagaMessage, IBasicProperties props = null)
        {
            _channel.QueueDeclare(queue: sagaMessage.EventName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            _channel.ExchangeDeclare(exchangeName, "direct", false, false);
            _channel.QueueBind(queue: sagaMessage.EventName, exchangeName, sagaMessage.EventName);

            var serializedMessage = JsonConvert.SerializeObject(objMessage);
            var body = System.Text.Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(exchange: exchangeName,
                                routingKey: sagaMessage.EventName,
                                basicProperties: props,
                                body: body);
        }

        private IModel OpenChannel()
        {
            return _rabbitMQBase.CreateBus().CreateModel();
        }

        public void CloseChannel()
        {
            if (_channel.IsOpen)
                _channel.Close();
        }
    }
}

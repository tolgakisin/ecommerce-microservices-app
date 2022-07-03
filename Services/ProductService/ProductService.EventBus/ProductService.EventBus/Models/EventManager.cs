using ProductService.EventBus.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ProductService.EventBus.Models
{
    public class EventManager : IEventManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQBase _rabbitMQBase;
        private readonly Dictionary<string, List<ISubscriber>> _subscribers = new Dictionary<string, List<ISubscriber>>();
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();

        public EventManager(IRabbitMQBase rabbitMQBase, IServiceProvider serviceProvider)
        {
            _rabbitMQBase = rabbitMQBase;
            _serviceProvider = serviceProvider;
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            string eventName = Utils.Utils.GetEventNameFromAttr<TEvent>();

            Subscriber<TEvent> subscriber = new Subscriber<TEvent>(_rabbitMQBase, _serviceProvider);

            if (_subscribers.TryGetValue(eventName, out var subList))
            {
                subList.Add(subscriber);
            }
            else
            {
                _subscribers.Add(eventName, new List<ISubscriber> { subscriber });
            }
        }

        public IEvent Publish<TEvent>(IEvent @event, string publishEventName = null)
        {
            @event.EventName = Utils.Utils.GetEventNameFromAttr(@event.GetType());

            using (var _channel = OpenChannel())
            {
                string eventName = publishEventName ?? Utils.Utils.GetEventNameFromAttr(@event.GetType());
                string exchangeName = "orchestrator-exchange";

                var props = _channel.CreateBasicProperties();
                if (@event.IsSync)
                {
                    var replyQueueName = _channel.QueueDeclare().QueueName;
                    var correlationId = Guid.NewGuid().ToString();

                    props.ReplyTo = replyQueueName;
                    props.CorrelationId = correlationId;

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);
                        var response = JsonConvert.DeserializeObject<BaseEvent>(bodyJson);

                        if (ea.BasicProperties.CorrelationId == correlationId)
                        {
                            _respQueue.Add(bodyJson);
                        }
                    };
                    _channel.BasicConsume(consumer: consumer, queue: replyQueueName, autoAck: true);
                }

                _channel.QueueDeclare(eventName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false);

                _channel.ExchangeDeclare(exchangeName, "direct", false, false);
                _channel.QueueBind(queue: eventName, exchangeName, eventName);

                var serializedMessage = JsonConvert.SerializeObject(@event);
                var body = System.Text.Encoding.UTF8.GetBytes(serializedMessage);

                _channel.BasicPublish(exchange: exchangeName,
                                    routingKey: eventName,
                                    basicProperties: props,
                                    body: body);

                if (@event.IsSync)
                {
                    var isTaken = _respQueue.TryTake(out var response, TimeSpan.FromSeconds(30));

                    return response == null ? null : JsonConvert.DeserializeObject<BaseEvent>(response);
                }

                return null;
            }
        }

        private IModel OpenChannel() => _rabbitMQBase.CreateConnection().CreateModel();
    }
}

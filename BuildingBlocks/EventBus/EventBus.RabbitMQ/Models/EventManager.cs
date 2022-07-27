using EventBus.Core;
using EventBus.RabbitMQ.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventBus.RabbitMQ.Models
{
    public class EventManager : IEventManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQBase _rabbitMQBase;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        public Dictionary<string, List<ISubscriber>> SubscriberList = new Dictionary<string, List<ISubscriber>>();

        public EventManager(IServiceProvider serviceProvider, IRabbitMQBase rabbitMQBase)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQBase = rabbitMQBase;
        }

        public void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>
        {
            string eventName = Utils.Utils.GetEventNameFromAttr<TEvent>();
            Subscriber<TEvent, TEventHandler> subscriber = new Subscriber<TEvent, TEventHandler>(_serviceProvider, _rabbitMQBase);
            if (SubscriberList.TryGetValue(eventName, out List<ISubscriber> subList))
            {
                subList.Add(subscriber);
            }
            else
            {
                subList = new List<ISubscriber>();
                subList.Add(subscriber);
                SubscriberList.Add(eventName, subList);
            }
        }

        public void UnSubscribe<TEvent>() where TEvent : IEvent
        {
            string eventName = Utils.Utils.GetEventNameFromAttr<TEvent>();
            if (SubscriberList.TryGetValue(eventName, out _))
            {
                SubscriberList.Remove(eventName);
            }
        }

        public TEvent Publish<TEvent>(TEvent @event, bool isOrchestration = true) where TEvent : IEvent
        {
            @event.EventName = Utils.Utils.GetEventNameFromAttr(@event.GetType()) ?? @event.GetType().Name;

            //TODO: Add retry policy.
            using (var _channel = OpenChannel())
            {
                string eventName = isOrchestration ? EventNames.OrchestratorGeneralEvent : Utils.Utils.GetEventNameFromAttr(@event.GetType());
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

                //TODO: Add retry policy.
                _channel.BasicPublish(exchange: exchangeName,
                                    routingKey: eventName,
                                    basicProperties: props,
                                    body: body);

                if (@event.IsSync)
                {
                    var isTaken = _respQueue.TryTake(out var response, TimeSpan.FromSeconds(200));

                    return response == null ? default : JsonConvert.DeserializeObject<TEvent>(response);
                }

                return default;
            }
        }

        private IModel OpenChannel()
        {
            return _rabbitMQBase.CreateBus().CreateModel();
        }
    }
}

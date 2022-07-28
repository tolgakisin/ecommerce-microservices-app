using EventBus.RabbitMQ.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ.Models
{
    public class Subscriber<TEvent, TEventHandler> : ISubscriber where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQBase _rabbitMQBase;
        private readonly IModel _channel;

        public Subscriber(IServiceProvider serviceProvider, IRabbitMQBase rabbitMQBase)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQBase = rabbitMQBase;
            _channel = OpenChannel();

            ReceiveAsync();
        }

        private void ReceiveAsync()
        {
            string eventName = Utils.Utils.GetEventNameFromAttr<TEvent>();
            _channel.QueueDeclare(queue: eventName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var props = ea.BasicProperties;
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);
                var message = JsonConvert.DeserializeObject<TEvent>(bodyJson);
                var messageDataBackup = JsonConvert.DeserializeObject<TEvent>(bodyJson);

                using (var serviceProvider = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var handler = serviceProvider.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));

                        if (message.EventFailed)
                        {
                            var concreteType = typeof(IEventHandler<>).MakeGenericType(typeof(TEvent));
                            await (Task)concreteType.GetMethod("HandleReverse").Invoke(handler, new object[] { message });

                            message.EventFinished = true;
                        }
                        else
                        {
                            var concreteType = typeof(IEventHandler<>).MakeGenericType(typeof(TEvent));
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { message });

                            message.EventFinished = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        message = messageDataBackup;
                        message.EventFinished = false;
                        message.EventFailed = true;
                        message.EventErrorMessage = ex.InnerException.Message;
                    }
                    finally
                    {
                        if (!string.IsNullOrEmpty(props.ReplyTo))
                        {
                            message.EventName = eventName;

                            var serializedMessage = JsonConvert.SerializeObject(message);
                            var responseBytes = Encoding.UTF8.GetBytes(serializedMessage);

                            //TODO: Add retry policy
                            _channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                        }
                    }
                }
            };

            _channel.BasicConsume(queue: eventName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private IModel OpenChannel() => _rabbitMQBase.CreateBus().CreateModel();
    }
}

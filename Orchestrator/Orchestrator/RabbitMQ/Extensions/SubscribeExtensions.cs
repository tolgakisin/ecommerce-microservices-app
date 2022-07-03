using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Orchestrator.Saga.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ.Extensions
{
    public static class SubscribeExtensions
    {
        private static readonly object _lock = new();

        public static void UseOrchestrationSubscription(this IApplicationBuilder app, IRabbitMQBase rabbitMQBase)
        {
            var _channel = rabbitMQBase.CreateBus().CreateModel();

            Consume(app, rabbitMQBase, _channel);
        }

        public static void Consume(IApplicationBuilder app, IRabbitMQBase rabbitMQBase, IModel channel)
        {
            if (channel == null || channel.IsClosed)
            {
                lock (_lock)
                {
                    if (channel == null || channel.IsClosed)
                    {
                        channel = rabbitMQBase.CreateBus().CreateModel();
                    }
                }
            }

            string eventName = "orchestrator-general-event";
            channel.QueueDeclare(queue: eventName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var rabbitBus = scope.ServiceProvider.GetService<IRabbitBus>();

                    var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);
                    var message = JsonConvert.DeserializeObject<SagaModel>(bodyJson);
                    SagaModel response = null;
                    try
                    {
                        response = await rabbitBus.SendMessageAsync(message);

                        if (message.IsSync)
                        {
                            //TODO: Add retry policy.
                            var props = ea.BasicProperties;
                            var replyProps = channel.CreateBasicProperties();
                            replyProps.CorrelationId = props.CorrelationId;

                            var serializedMessage = JsonConvert.SerializeObject(response);
                            var responseBytes = Encoding.UTF8.GetBytes(serializedMessage);

                            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            };

            channel.BasicConsume(queue: eventName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}

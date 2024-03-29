﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchestrator.Data.Common;
using Orchestrator.Data.Entities;
using Orchestrator.Saga.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Orchestrator.RabbitMQ.Extensions
{
    public static class SubscribeExtensions
    {
        private static readonly object _lock = new();

        public static void UseOrchestrationSubscription(this IApplicationBuilder app, IRabbitMQBase rabbitMQBase)
        {
            var _channel = rabbitMQBase.CreateBus().CreateModel();

            Consume(app, _channel);
        }

        public static void Consume(IApplicationBuilder app, IModel channel)
        {
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
                    var context = scope.ServiceProvider.GetService<Context>();

                    var bodyJson = System.Text.Encoding.UTF8.GetString(ea.Body.Span);

                    var objMessage = JObject.Parse(bodyJson);
                    var sagaMessage = objMessage.ToObject<SagaModel>();

                    try
                    {
                        var @event = context.GetEventByName(sagaMessage.EventName);
                        if (@event == null)
                        {
                            context.Save<EventLog>(new EventLog
                            {
                                EventId = sagaMessage.EventId,
                                Data = bodyJson,
                                ExecutionDate = DateTime.Now,
                                State = Common.Enums.EventState.NotFound,
                                ErrorMessage = $"{sagaMessage.EventName} is not found."
                            });

                            if (sagaMessage.EventSync)
                                SyncPublish(channel, ea.BasicProperties, null);

                            rabbitBus.CloseChannel();

                            return;
                        }

                        objMessage[nameof(sagaMessage.EventId)] = @event.Id;
                        sagaMessage.EventId = @event.Id;


                        context.Save<EventLog>(new EventLog
                        {
                            EventId = sagaMessage.EventId,
                            Data = JsonConvert.SerializeObject(objMessage),
                            ExecutionDate = DateTime.Now,
                            State = Common.Enums.EventState.Started
                        });

                        var response = await rabbitBus.SendMessageAsync(objMessage, sagaMessage, app);

                        if (sagaMessage.EventSync)
                            SyncPublish(channel, ea.BasicProperties, response);
                    }
                    catch (Exception ex)
                    {
                        context.Save<EventLog>(new EventLog
                        {
                            EventId = sagaMessage.EventId,
                            Data = bodyJson,
                            ExecutionDate = DateTime.Now,
                            State = Common.Enums.EventState.Error,
                            ErrorMessage = ex.Message
                        });
                    }
                }
            };

            channel.BasicConsume(queue: eventName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private static void SyncPublish(IModel channel, IBasicProperties props, object response)
        {
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            var serializedMessage = JsonConvert.SerializeObject(response);
            var responseBytes = Encoding.UTF8.GetBytes(serializedMessage);

            channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
        }
    }
}

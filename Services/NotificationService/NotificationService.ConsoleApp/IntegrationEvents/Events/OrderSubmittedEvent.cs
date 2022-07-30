using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace NotificationService.ConsoleApp.IntegrationEvents.Events
{
    [Event(EventNames.Order.OrderSubmittedEvent)]
    public class OrderSubmittedEvent : BaseEvent
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

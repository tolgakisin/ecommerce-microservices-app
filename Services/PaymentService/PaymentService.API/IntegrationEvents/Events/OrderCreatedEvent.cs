using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.Order.OrderCreatedEvent)]
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent(Guid userId, Guid orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

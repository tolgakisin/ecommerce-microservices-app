using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace OrderService.Application.IntegrationEvents
{
    [Event(EventNames.Order.OrderSubmittedEvent)]
    public class OrderSubmittedEvent : BaseEvent
    {
        public OrderSubmittedEvent(Guid userId, Guid orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

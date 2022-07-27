using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace OrderService.API.IntegrationEvents.Events
{
    [Event(EventNames.OrderStartedEvent)]
    public class OrderStartedEvent : BaseEvent
    {
        public OrderStartedEvent()
        {

        }

        public OrderStartedEvent(Guid userId, Guid orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

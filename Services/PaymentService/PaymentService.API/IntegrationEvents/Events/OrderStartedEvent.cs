using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using PaymentService.API.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.Order.OrderStartedEvent)]
    public class OrderStartedEvent : BaseEvent
    {
        public OrderStartedEvent(Guid userId, Guid orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace OrderService.Application.IntegrationEvents.Events
{
    [Event(EventNames.Payment.PaymentFailedEvent)]
    public class PaymentFailedEvent : BaseEvent
    {
        public Guid OrderId { get; set; }

        public PaymentFailedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

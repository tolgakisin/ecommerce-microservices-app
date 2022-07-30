using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace OrderService.Application.IntegrationEvents
{
    [Event(EventNames.Payment.PaymentSuccessEvent)]
    public class PaymentFailedEvent : BaseEvent
    {
        public Guid OrderId { get; set; }

        public PaymentFailedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

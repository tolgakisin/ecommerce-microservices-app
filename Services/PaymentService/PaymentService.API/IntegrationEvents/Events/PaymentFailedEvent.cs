using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.Payment.PaymentFailedEvent)]
    public class PaymentFailedEvent : BaseEvent
    {
        public PaymentFailedEvent(Guid userId, Guid orderId, string validationMessage)
        {
            UserId = userId;
            OrderId = orderId;
            ValidationMessage = validationMessage;
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string ValidationMessage { get; set; }
    }
}

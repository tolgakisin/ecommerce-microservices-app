using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace NotificationService.ConsoleApp.IntegrationEvents.Events
{
    [Event(EventNames.Payment.PaymentFailedEvent)]
    public class PaymentFailedEvent : BaseEvent
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string ValidationMessage { get; set; }
    }
}

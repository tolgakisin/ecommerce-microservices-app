using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace OrderService.API.IntegrationEvents.Events
{
    [Event(EventNames.PaymentSuccessEvent)]
    public class PaymentSuccessEvent : BaseEvent
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}

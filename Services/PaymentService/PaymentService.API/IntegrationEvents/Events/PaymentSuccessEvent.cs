﻿using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.Payment.PaymentSuccessEvent)]
    public class PaymentSuccessEvent : BaseEvent
    {
        public PaymentSuccessEvent(Guid userId, Guid orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public Guid UserId { get; }
        public Guid OrderId { get; set; }
    }
}

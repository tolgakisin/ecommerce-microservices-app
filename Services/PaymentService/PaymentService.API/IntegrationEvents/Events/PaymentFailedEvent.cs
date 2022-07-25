﻿using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using PaymentService.API.EventModels;
using System;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.PaymentFailedEvent)]
    public class PaymentFailedEvent : BaseEvent
    {
        public PaymentFailedEvent(Guid userId, string validationMessage)
        {
            UserId = userId;
            ValidationMessage = validationMessage;
        }

        public Guid UserId { get; set; }
        public string ValidationMessage { get; set; }
    }
}
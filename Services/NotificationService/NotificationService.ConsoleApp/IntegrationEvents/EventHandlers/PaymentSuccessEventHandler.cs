﻿using EventBus.RabbitMQ.Models;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class PaymentSuccessEventHandler : IEventHandler<PaymentSuccessEvent>
    {
        public Task Handle(PaymentSuccessEvent @event)
        {
            Console.WriteLine($"Payment Success. UserId:{@event.UserId} OrderId:{@event.OrderId}");

            return Task.CompletedTask;
        }

        public Task HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

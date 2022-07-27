using EventBus.RabbitMQ.Models;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class PaymentSuccessEventHandler : IEventHandler<PaymentSuccessEvent>
    {
        public Task Handle(PaymentSuccessEvent @event)
        {
            Console.WriteLine($"Payment success. UserId:{@event.UserId} OrderId:{@event.OrderId}");

            return Task.CompletedTask;
        }

        public Task HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

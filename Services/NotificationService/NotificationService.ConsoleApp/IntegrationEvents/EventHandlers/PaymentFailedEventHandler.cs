using EventBus.RabbitMQ.Models;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class PaymentFailedEventHandler : IEventHandler<PaymentFailedEvent>
    {
        public Task Handle(PaymentFailedEvent @event)
        {
            Console.WriteLine($"Payment Failed. UserId:{@event.UserId} OrderId:{@event.OrderId} Message:{@event.ValidationMessage}");

            return Task.CompletedTask;
        }

        public Task HandleReverse(PaymentFailedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

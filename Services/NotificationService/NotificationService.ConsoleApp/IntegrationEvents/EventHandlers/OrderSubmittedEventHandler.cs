using EventBus.RabbitMQ.Models;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace NotificationService.ConsoleApp.IntegrationEvents.EventHandlers
{
    public class OrderSubmittedEventHandler : IEventHandler<OrderSubmittedEvent>
    {
        public Task Handle(OrderSubmittedEvent @event)
        {
            Console.WriteLine($"Order Submitted. UserId:{@event.UserId} OrderId:{@event.OrderId}");

            return Task.CompletedTask;
        }

        public Task HandleReverse(OrderSubmittedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

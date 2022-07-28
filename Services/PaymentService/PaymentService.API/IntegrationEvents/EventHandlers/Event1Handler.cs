using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.Events
{
    public class Event1Handler : IEventHandler<Event1>
    {
        public Task Handle(Event1 @event)
        {
            @event.Name += "Event1 Handle.";

            return Task.CompletedTask;
        }

        public Task HandleReverse(Event1 @event)
        {
            @event.Name += "Event1 Reverse.";

            return Task.CompletedTask;
        }
    }
}

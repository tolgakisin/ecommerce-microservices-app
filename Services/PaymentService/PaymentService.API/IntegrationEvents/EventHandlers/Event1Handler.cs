using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.Events
{
    public class Event1Handler : IEventHandler<Event1>
    {
        public Task Handle(Event1 @event)
        {
            @event.Data += "Event1 is started.";

            return Task.CompletedTask;
        }

        public Task HandleReverse(Event1 @event)
        {
            @event.Data += "Event1 is reversed.";

            return Task.CompletedTask;
        }
    }
}

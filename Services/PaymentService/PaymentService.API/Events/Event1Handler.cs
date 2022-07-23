using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.API.Events
{
    public class Event1Handler : IEventHandler<Event1>
    {
        public Task<Event1> Handle(Event1 @event)
        {
            @event.Data += "Event1 is started.";

            return Task.FromResult(@event);
        }

        public Task<Event1> HandleReverse(Event1 @event)
        {
            @event.Data += "Event1 is reversed.";

            return Task.FromResult(@event);
        }
    }
}

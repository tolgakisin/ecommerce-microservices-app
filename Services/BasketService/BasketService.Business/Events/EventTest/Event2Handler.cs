using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.Events.EventTest
{
    public class Event2Handler : IEventHandler<Event2>
    {
        public Task<Event2> Handle(Event2 @event)
        {
            @event.Data += "Event2 is started.";
            throw new Exception("An error occured.");

            return Task.FromResult(@event);
        }

        public Task<Event2> HandleReverse(Event2 @event)
        {
            @event.Data += "Event2 reverse is started.";

            return Task.FromResult(@event);
        }
    }
}

using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.IntegrationEvents.Events.EventTest
{
    public class Event2Handler : IEventHandler<Event2>
    {
        public Task Handle(Event2 @event)
        {
            @event.Data += "Event2 is started.";
            //throw new Exception("An error occured.");

            return Task.CompletedTask;
        }

        public Task HandleReverse(Event2 @event)
        {
            @event.Data += "Event2 reverse is started.";

            return Task.CompletedTask;
        }
    }
}

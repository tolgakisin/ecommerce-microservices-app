using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.IntegrationEvents.Events.EventTest
{
    public class Event2Handler : IEventHandler<Event2>
    {
        public Task Handle(Event2 @event)
        {
            @event.Surname += "Event2 Handle.";
            throw new Exception("An error occured. Event2");

            return Task.CompletedTask;
        }

        public Task HandleReverse(Event2 @event)
        {
            @event.Surname += "Event2 Reverse.";

            return Task.CompletedTask;
        }
    }
}

using BasketService.Business.IntegrationEvents.Events.EventTest;
using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Business.IntegrationEvents.EventHandlers
{
    public class Event1Handler : IEventHandler<Event1>
    {
        public Task<Event1> Handle(Event1 @event)
        {
            var data = @event;

            return Task.FromResult(@event);
        }

        public Task<Event1> HandleReverse(Event1 @event)
        {
            throw new NotImplementedException();
        }
    }
}

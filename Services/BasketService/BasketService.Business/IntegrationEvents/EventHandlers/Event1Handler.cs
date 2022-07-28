﻿using BasketService.Business.IntegrationEvents.Events.EventTest;
using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.IntegrationEvents.EventHandlers
{
    public class Event1Handler : IEventHandler<Event1>
    {
        public Task Handle(Event1 @event)
        {
            var data = @event;
            @event.Name += "Event1 Handle.";

            return Task.CompletedTask;
        }

        public Task HandleReverse(Event1 @event)
        {
            return Task.CompletedTask;
        }
    }
}

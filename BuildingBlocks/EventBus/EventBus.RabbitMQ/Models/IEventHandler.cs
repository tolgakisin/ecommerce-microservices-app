﻿using System.Threading.Tasks;

namespace EventBus.RabbitMQ.Models
{
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IEvent
    {
        Task Handle(TEvent @event);
        Task HandleReverse(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}

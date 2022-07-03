using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.EventBus.Models
{
    public interface IEventManager
    {
        void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
        IEvent Publish<TEvent>(IEvent @event, string publishEventName = null);
    }
}

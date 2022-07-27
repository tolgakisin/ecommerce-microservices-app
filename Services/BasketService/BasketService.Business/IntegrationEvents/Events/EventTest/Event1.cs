using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace BasketService.Business.IntegrationEvents.Events.EventTest
{
    [Event(EventNames.Event1)]
    public class Event1 : BaseEvent
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

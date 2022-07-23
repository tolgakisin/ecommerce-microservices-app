using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace BasketService.Business.Events.EventTest
{
    [Event(EventNames.Event1)]
    public class Event1 : BaseEvent
    {
    }
}

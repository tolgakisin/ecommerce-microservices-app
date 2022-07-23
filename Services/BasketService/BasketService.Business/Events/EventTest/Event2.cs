using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace BasketService.Business.Events.EventTest
{
    [Event(EventNames.Event2)]
    public class Event2 : BaseEvent
    {
    }
}

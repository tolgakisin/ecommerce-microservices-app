using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace BasketService.Business.Events.EventTest
{
    [Event("Event2")]
    public class Event2 : BaseEvent
    {
    }
}

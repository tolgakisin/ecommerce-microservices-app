using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.Event1)]
    public class Event1 : BaseEvent
    {
    }
}

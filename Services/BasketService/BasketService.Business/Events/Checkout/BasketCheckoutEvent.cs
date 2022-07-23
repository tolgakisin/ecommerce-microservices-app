using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;

namespace BasketService.Business.Events.Checkout
{
    [Event("BasketCheckoutEvent")]
    public class BasketCheckoutEvent : BaseEvent
    {
    }
}

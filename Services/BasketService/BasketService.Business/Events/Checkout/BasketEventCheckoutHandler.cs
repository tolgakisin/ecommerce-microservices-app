using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.Events.Checkout
{
    public class BasketEventCheckoutHandler : IEventHandler<BasketCheckoutEvent>
    {
        public Task<BasketCheckoutEvent> Handle(BasketCheckoutEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task<BasketCheckoutEvent> HandleReverse(BasketCheckoutEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

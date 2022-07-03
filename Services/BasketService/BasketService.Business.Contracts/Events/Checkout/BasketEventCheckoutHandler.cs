using BasketService.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Business.Contracts.Events.Checkout
{
    public class BasketEventCheckoutHandler : IEventHandler<BasketCheckoutEvent>
    {
        public Task Handle(BasketCheckoutEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task HandleReverse(BasketCheckoutEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

using BasketService.Business.Contracts.Services;
using BasketService.Business.Events.Checkout;
using EventBus.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Business.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IBasketService _basketService;

        public OrderCreatedEventHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<OrderCreatedEvent> Handle(OrderCreatedEvent @event)
        {
            await _basketService.ClearBasketAsync(@event.UserId);

            return @event;
        }

        public Task<OrderCreatedEvent> HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

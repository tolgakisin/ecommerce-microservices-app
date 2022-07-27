using BasketService.Business.Contracts.Services;
using BasketService.Business.IntegrationEvents.Events;
using EventBus.RabbitMQ.Models;
using System;
using System.Threading.Tasks;

namespace BasketService.Business.IntegrationEvents.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IBasketService _basketService;

        public OrderCreatedEventHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public Task Handle(OrderCreatedEvent @event)
        {
            //await _basketService.ClearBasketAsync(@event.UserId.ToString());

            return Task.CompletedTask;
        }

        public Task HandleReverse(OrderCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

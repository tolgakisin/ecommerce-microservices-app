using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.Features.Commands.CreateOrder;
using OrderService.Application.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents.EventHandlers
{
    public class OrderStartedEventHandler : IEventHandler<OrderStartedEvent>
    {
        private readonly IMediator _mediator;

        public OrderStartedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderStartedEvent @event)
        {
            _ = await _mediator.Send(new CreateOrderCommand(@event.UserId, @event.CustomerBasket, @event.CustomerAddress, @event.CustomerPayment));
        }

        public Task HandleReverse(OrderStartedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

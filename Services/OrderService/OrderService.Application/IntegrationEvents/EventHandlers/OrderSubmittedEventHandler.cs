using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.Features.Commands.ChangeOrderStatus;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents.EventHandlers
{
    public class OrderSubmittedEventHandler : IEventHandler<OrderSubmittedEvent>
    {
        private readonly IMediator _mediator;

        public OrderSubmittedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderSubmittedEvent @event)
        {
            _ = await _mediator.Send(new ChangeOrderStatusCommand(@event.OrderId, OrderStatus.Submitted));
        }

        public Task HandleReverse(OrderSubmittedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

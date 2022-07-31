using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.Features.Commands.ChangeOrderStatus;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents.EventHandlers
{
    public class PaymentFailedEventHandler : IEventHandler<PaymentFailedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentFailedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PaymentFailedEvent @event)
        {
            _ = await _mediator.Send(new ChangeOrderStatusCommand(@event.OrderId, OrderStatus.Cancelled));
        }

        public Task HandleReverse(PaymentFailedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.Features.Commands.ChangeOrderStatus;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents.EventHandlers
{
    public class PaymentSuccessEventHandler : IEventHandler<PaymentSuccessEvent>
    {
        private readonly IEventManager _eventManager;
        private readonly IMediator _mediator;

        public PaymentSuccessEventHandler(IEventManager eventManager, IMediator mediator)
        {
            _eventManager = eventManager;
            _mediator = mediator;
        }

        public async Task Handle(PaymentSuccessEvent @event)
        {
            var order = await _mediator.Send(new ChangeOrderStatusCommand(@event.OrderId, OrderStatus.Paid));

            if (order != null)
                _eventManager.Publish(new OrderSubmittedEvent(order.UserId, order.Id));
        }

        public Task HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

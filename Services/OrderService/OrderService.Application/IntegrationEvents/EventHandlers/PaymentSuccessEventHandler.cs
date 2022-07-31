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
        private readonly IMediator _mediator;
        private readonly IEventManager _eventManager;

        public PaymentSuccessEventHandler(IMediator mediator, IEventManager eventManager)
        {
            _mediator = mediator;
            _eventManager = eventManager;
        }

        public async Task Handle(PaymentSuccessEvent @event)
        {
            var order = await _mediator.Send(new ChangeOrderStatusCommand(@event.OrderId, OrderStatus.Paid));

            if (order != null)
                _eventManager.Publish(new OrderSubmittedEvent(order.BuyerId, order.Id));
        }

        public Task HandleReverse(PaymentSuccessEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

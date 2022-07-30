using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.PaymentSuccessCommand
{
    public class PaymentSuccessCommandHandler : IRequestHandler<PaymentSuccessCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventManager _eventManager;

        public PaymentSuccessCommandHandler(IOrderRepository orderRepository, IEventManager eventManager)
        {
            _orderRepository = orderRepository;
            _eventManager = eventManager;
        }

        public async Task<bool> Handle(PaymentSuccessCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            order.SetStatus(OrderStatus.Paid);

            var result = await _orderRepository.SaveAsync(order);
            _eventManager.Publish(new OrderSubmittedEvent(order.BuyerId, order.Id));

            return result != null;
        }
    }
}

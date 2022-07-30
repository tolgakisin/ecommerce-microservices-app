using MediatR;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.PaymentFailedCommand
{
    public class PaymentFailedCommandHandler : IRequestHandler<PaymentFailedCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public PaymentFailedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(PaymentFailedCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            order.SetStatus(OrderStatus.Cancelled);

            return (await _orderRepository.SaveAsync(order)) != null;
        }
    }
}

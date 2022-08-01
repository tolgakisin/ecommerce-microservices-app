using MediatR;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            order.SetStatus(request.OrderStatus);

            var result = await _orderRepository.SaveAsync(order);

            return result;
        }
    }
}

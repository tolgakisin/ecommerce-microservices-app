using MediatR;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;

namespace OrderService.Application.Features.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand : IRequest<Order>
    {
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public ChangeOrderStatusCommand(Guid orderId, OrderStatus orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
        }
    }
}

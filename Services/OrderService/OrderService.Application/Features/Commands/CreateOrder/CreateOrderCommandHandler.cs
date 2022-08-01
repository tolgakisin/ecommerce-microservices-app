using EventBus.RabbitMQ.Models;
using MediatR;
using OrderService.Application.IntegrationEvents.Events;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventManager _eventManager;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventManager eventManager)
        {
            _orderRepository = orderRepository;
            _eventManager = eventManager;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.OrderAddress.City, request.OrderAddress.Street, request.OrderAddress.State, request.OrderAddress.Country, request.OrderAddress.ZipCode);
            var payment = new Payment(request.OrderPayment.CardNumber, request.OrderPayment.CardHolderName, request.OrderPayment.ExpirationDate, request.OrderPayment.CardSecurityNumber, request.OrderPayment.CardTypeId);
            var order = new Order(request.UserId, address, payment);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);
            }

            var result = await _orderRepository.SaveAsync(order);

            _eventManager.Publish(new OrderCreatedEvent(result.UserId, result.Id));

            return result != null;
        }
    }
}

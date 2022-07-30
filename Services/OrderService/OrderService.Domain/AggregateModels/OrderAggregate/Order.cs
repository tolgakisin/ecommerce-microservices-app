using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; private set; }
        public string Description { get; private set; }
        public Guid BuyerId { get; private set; }
        public Address Address { get; private set; }
        private int orderStatusId { get; set; }
        public OrderStatus OrderStatus { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Guid PaymentId { get; private set; }
        public Payment Payment { get; private set; }

        protected Order()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        public Order(Guid buyerId, Address address, Payment payment)
        {
            BuyerId = buyerId;
            OrderDate = DateTime.Now;
            Address = address;
            Payment = payment;
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            // OrderItem Validations

            _orderItems.Add(new OrderItem(productId, productName, unitPrice, quantity));
        }

        public void SetBuyerId(Guid buyerId)
        {
            BuyerId = buyerId;
        }

        public void SetPaymentId(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}

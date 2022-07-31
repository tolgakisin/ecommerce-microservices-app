using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Domain.Common;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; private set; }
        public string Description { get; private set; }
        public Guid BuyerId { get; private set; }
        public Buyer Buyer { get; set; }
        public Address Address { get; private set; }
        public int OrderStatus { get; private set; }
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

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            // OrderItem Validations

            _orderItems.Add(new OrderItem(productId, productName, unitPrice, quantity, Id));
        }

        public void SetBuyerId(Guid buyerId)
        {
            BuyerId = buyerId;
        }

        public void SetPaymentId(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public void SetStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus.Id;
        }
    }
}

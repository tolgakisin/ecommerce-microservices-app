using OrderService.Domain.Common;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AddressId { get; private set; }
        public virtual Address Address { get; private set; }
        public int OrderStatus { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }
        public Guid PaymentId { get; private set; }
        public virtual Payment Payment { get; private set; }

        protected Order()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;
        }

        public Order(Guid userId, Address address, Payment payment)
        {
            OrderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;

            OrderStatus = OrderAggregate.OrderStatus.Pending.Id;
            UserId = userId;
            Address = address;
            AddressId = address.Id;
            Payment = payment;
            PaymentId = payment.Id;
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            // OrderItem Validations

            OrderItems.Add(new OrderItem(productId, productName, unitPrice, quantity, Id));
        }

        public void SetBuyerId(Guid userId)
        {
            UserId = userId;
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

using OrderService.Domain.Common;
using System;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;

            if (productId == default)
                throw new Exception("Product is not found.");

            if (quantity < 1)
                throw new Exception("Invalid number of units.");
        }

        protected OrderItem()
        {

        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}

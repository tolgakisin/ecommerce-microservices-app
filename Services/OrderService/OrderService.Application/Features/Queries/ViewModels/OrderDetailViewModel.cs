using System;
using System.Collections.Generic;

namespace OrderService.Application.Features.Queries.ViewModels
{
    public class OrderDetailViewModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }

    public class OrderAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}

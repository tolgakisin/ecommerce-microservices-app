using System;
using System.Collections.Generic;

namespace OrderService.Domain.Models
{
    public class CustomerBasket
    {
        public Guid BuyerId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}

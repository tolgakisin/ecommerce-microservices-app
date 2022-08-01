using System;
using System.Collections.Generic;

namespace OrderService.Domain.Models
{
    public class CustomerBasket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}

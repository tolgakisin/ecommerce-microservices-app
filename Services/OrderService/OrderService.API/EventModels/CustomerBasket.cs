using System;
using System.Collections.Generic;

namespace OrderService.API.EventModels
{
    public class CustomerBasket
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}

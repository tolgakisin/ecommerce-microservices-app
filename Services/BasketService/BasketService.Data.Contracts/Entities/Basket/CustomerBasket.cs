using BasketService.Data.Contracts.Base;
using System;
using System.Collections.Generic;

namespace BasketService.Data.Contracts.Entities.Basket
{
    public class CustomerBasket : BaseEntity
    {
        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace PaymentService.API.EventModels
{
    public class CustomerBasket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}

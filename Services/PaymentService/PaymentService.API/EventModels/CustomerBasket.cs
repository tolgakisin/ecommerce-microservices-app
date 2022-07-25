using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.EventModels
{
    public class CustomerBasket
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}

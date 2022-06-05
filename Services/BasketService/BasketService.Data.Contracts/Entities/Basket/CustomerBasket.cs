using BasketService.Data.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Data.Contracts.Entities.Basket
{
    public class CustomerBasket : BaseEntity
    {
        public Guid BuyerId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}

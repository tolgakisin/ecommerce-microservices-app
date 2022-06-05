using BasketService.API.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Models
{
    public class CustomerBasketModel : BaseModel
    {
        public Guid BuyerId { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}

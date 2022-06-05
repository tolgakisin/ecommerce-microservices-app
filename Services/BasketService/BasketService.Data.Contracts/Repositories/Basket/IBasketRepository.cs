using BasketService.Data.Contracts.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Data.Contracts.Repositories.Basket
{
    public interface IBasketRepository
    {
        Task<bool> DeleteBasketAsync(string userId);
        Task<CustomerBasket> GetBasketAsync(string userId);
        Task<bool> UpdateBasketAsync(CustomerBasket basket);
    }
}

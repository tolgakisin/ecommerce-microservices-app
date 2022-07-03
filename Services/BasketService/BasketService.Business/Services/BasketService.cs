using BasketService.Business.Contracts.Services;
using BasketService.Common.Utils;
using BasketService.Data.Contracts.Entities.Basket;
using BasketService.Data.Contracts.Repositories.Basket;
using BasketService.Data.Repositories.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasket> GetBasketAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
                ErrorManagement.ThrowError("Buyer is not found.");

            return await _basketRepository.GetBasketAsync(buyerId);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            if (customerBasket == null || customerBasket.BuyerId == Guid.Empty)
                ErrorManagement.ThrowError("Basket is not found.");

            bool isUpdated = await _basketRepository.UpdateBasketAsync(customerBasket);

            if (!isUpdated)
                ErrorManagement.ThrowError("Basket couldn't be updated.");

            return await GetBasketAsync(customerBasket.BuyerId.ToString());
        }

        public async Task<bool> ClearBasketAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
                ErrorManagement.ThrowError("Buyer is not found.");

            bool isCleared = await _basketRepository.DeleteBasketAsync(buyerId);

            if (!isCleared)
                ErrorManagement.ThrowError("Basket couldn't be cleared.");

            return isCleared;
        }

        public async Task<CustomerBasket> CheckoutBasketAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
                ErrorManagement.ThrowError("Buyer is not found.");

            CustomerBasket customerBasket = await _basketRepository.GetBasketAsync(buyerId);

            if (customerBasket == null || !customerBasket.BasketItems.Any())
                ErrorManagement.ThrowError("Basket couldn't be sent.");

            // Check Stock

            // Send BasketCheckoutEvent

            return customerBasket;
        }
    }
}

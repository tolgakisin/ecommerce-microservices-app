﻿using BasketService.Data.Contracts.Entities.Basket;
using BasketService.Data.Contracts.FakeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Business.Contracts.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetBasketAsync(string buyerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> ClearBasketAsync(string buyerId);
        Task<CustomerBasket> CheckoutBasketAsync(string buyerId, CustomerAddress customerAddress, CustomerPayment customerPayment);
        Task TestEventBusAndOrchestration();
    }
}

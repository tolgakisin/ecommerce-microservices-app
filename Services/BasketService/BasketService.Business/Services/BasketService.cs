﻿using BasketService.Business.Contracts.Services;
using BasketService.Business.IntegrationEvents.Events;
using BasketService.Common.Utils;
using BasketService.Data.Contracts.Entities.Basket;
using BasketService.Data.Contracts.FakeEntities;
using BasketService.Data.Contracts.Repositories.Basket;
using EventBus.RabbitMQ.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IEventManager _eventManager;

        public BasketService(IBasketRepository basketRepository, IEventManager eventManager)
        {
            _basketRepository = basketRepository;
            _eventManager = eventManager;
        }

        public async Task<CustomerBasket> GetBasketAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
                ErrorManagement.ThrowError("Buyer is not found.");

            return await _basketRepository.GetBasketAsync(buyerId);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            if (customerBasket == null || customerBasket.UserId == Guid.Empty)
                ErrorManagement.ThrowError("Basket is not found.");

            bool isUpdated = await _basketRepository.UpdateBasketAsync(customerBasket);

            if (!isUpdated)
                ErrorManagement.ThrowError("Basket couldn't be updated.");

            return await GetBasketAsync(customerBasket.UserId.ToString());
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

        public async Task<CustomerBasket> CheckoutBasketAsync(string buyerId, CustomerAddress customerAddress, CustomerPayment customerPayment)
        {
            if (string.IsNullOrEmpty(buyerId))
                ErrorManagement.ThrowError("Buyer is not found.");

            CustomerBasket customerBasket = await _basketRepository.GetBasketAsync(buyerId);

            if (customerBasket == null || !customerBasket.BasketItems.Any())
                ErrorManagement.ThrowError("Basket couldn't be sent.");

            _eventManager.Publish(new OrderStartedEvent(Guid.Parse(buyerId), customerBasket, customerAddress, customerPayment));

            return customerBasket;
        }

        public Task TestEventBusAndOrchestration()
        {
            //Event1 event1 = new Event1()
            //{
            //    Name = "Tolga",
            //    Surname = "TestSurname",
            //    //EventSync = true
            //};

            //var result = _eventManager.Publish(event1);

            return Task.CompletedTask;
        }
    }
}

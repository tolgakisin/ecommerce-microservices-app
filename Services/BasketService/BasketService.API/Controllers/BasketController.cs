using BasketService.API.Contracts.Requests;
using BasketService.API.Contracts.Responses;
using BasketService.API.Controllers.Base;
using BasketService.Business.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<CustomerBasketResponse> GetBasketAsync()
        {
            return await base.ExecuteAsync<CustomerBasketResponse>(async (response) =>
            {
                response.SetModels(await _basketService.GetBasketAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));

                return response;
            });
        }

        [HttpPost]
        public async Task<CustomerBasketResponse> UpdateBasketAsync(CustomerBasketRequest request)
        {
            return await base.ExecuteAsync<CustomerBasketResponse>(async (response) =>
            {
                response.SetModels(await _basketService.UpdateBasketAsync(request.GetEntities().FirstOrDefault()));

                return response;
            });
        }

        [HttpPost]
        public async Task<CustomerBasketResponse> ClearBasketAsync()
        {
            return await base.ExecuteAsync<CustomerBasketResponse>(async (response) =>
            {
                _ = await _basketService.ClearBasketAsync(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

                return response;
            });
        }

        [HttpPost]
        public async Task<CustomerBasketResponse> CheckoutBasketAsync()
        {
            return await base.ExecuteAsync<CustomerBasketResponse>(async (response) =>
            {
                //TODO Add EventBus Architecture and continue the process.

                return response;
            });
        }
    }
}

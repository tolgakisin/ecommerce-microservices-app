using BasketService.Data.Contracts.Entities.Basket;
using BasketService.Data.Contracts.Repositories.Basket;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Data.Repositories.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redisDb;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string userId)
        {
            return await _redisDb.KeyDeleteAsync(userId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string userId)
        {
            var data = await _redisDb.StringGetAsync(userId);

            if (data.IsNullOrEmpty) return null;

            return JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<bool> UpdateBasketAsync(CustomerBasket basket)
        {
            if (basket.Id == Guid.Empty)
            {
                basket.Id = Guid.NewGuid();
            }

            foreach (var item in basket.BasketItems.Where(basket => basket.Id == Guid.Empty))
            {
                item.Id = Guid.NewGuid();
            }

            return await _redisDb.StringSetAsync(basket.UserId.ToString(), JsonConvert.SerializeObject(basket));
        }
    }
}

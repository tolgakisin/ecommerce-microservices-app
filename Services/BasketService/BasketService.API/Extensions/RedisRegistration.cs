using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Extensions
{
    public static class RedisRegistration
    {
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services, IConfiguration configuration)
        {
            return ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]);
        }
    }
}

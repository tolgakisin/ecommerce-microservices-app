using EventBus.RabbitMQ.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Extensions
{
    public static class EventHandlerRegistration
    {
        public static void AddEventHandlers(this IServiceCollection services)
        {
            var handlers = typeof(BasketService.Business.Services.BasketService).Assembly.GetTypes()
                .Where(x => x.IsClass && x.GetInterface(nameof(IEventHandler)) == typeof(IEventHandler));

            foreach (var item in handlers)
            {
                Type genericInterface = item.GetInterfaces().FirstOrDefault(x => x.IsGenericType);
                Type genericType = genericInterface.GetGenericTypeDefinition();

                services.Add(new ServiceDescriptor(genericInterface, item, ServiceLifetime.Scoped));
            }
        }
    }
}

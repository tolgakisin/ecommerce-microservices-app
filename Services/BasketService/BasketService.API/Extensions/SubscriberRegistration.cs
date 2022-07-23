using EventBus.RabbitMQ.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BasketService.API.Extensions
{
    public static class SubscriberRegistration
    {
        public static void UseEventSubscribing(this IApplicationBuilder application)
        {
            var eventAggregator = application.ApplicationServices.GetRequiredService<IEventManager>();
            var events = typeof(BasketService.Business.Services.BasketService).Assembly.GetTypes().Where(x => x.IsClass && x.BaseType == typeof(BaseEvent));
            var eventHandlers = typeof(BasketService.Business.Services.BasketService).Assembly.GetTypes().Where(x => x.GetInterface(nameof(IEventHandler)) == typeof(IEventHandler));
            foreach (var @event in events)
            {
                var handlerOfEvent = eventHandlers.FirstOrDefault(x => x.GetInterfaces().Any(i => i.GenericTypeArguments.Any(g => g.Name == @event.Name)));
                if (handlerOfEvent != null)
                {
                    var eventAggregatorType = typeof(IEventManager);
                    eventAggregatorType.GetMethod("Subscribe").MakeGenericMethod(new Type[] { @event, handlerOfEvent }).Invoke(eventAggregator, null);
                }
            }
        }
    }
}

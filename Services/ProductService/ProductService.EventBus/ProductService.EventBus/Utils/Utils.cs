using ProductService.EventBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.EventBus.Utils
{
    public static class Utils
    {
        public static string GetEventNameFromAttr<TEvent>() where TEvent : IEvent
        {
            return typeof(TEvent).GetCustomAttribute<EventAttribute>().Value;
        }
        public static string GetEventNameFromAttr(Type type)
        {
            return type.GetCustomAttribute<EventAttribute>().Value;
        }
    }
}

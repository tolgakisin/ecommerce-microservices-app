using EventBus.RabbitMQ.Models;
using System;
using System.Linq;
using System.Reflection;

namespace EventBus.RabbitMQ.Utils
{
    public class Utils
    {
        public static string GetEventNameFromAttr<TEvent>() where TEvent : IEvent
        {
            EventAttribute attr = (EventAttribute)typeof(TEvent).GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(EventAttribute));

            return attr.Value;
        }

        public static string GetEventNameFromAttr(Type type)
        {
            EventAttribute attr = (EventAttribute)type.GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(EventAttribute));

            return attr.Value;
        }
    }
}

using System;

namespace EventBus.RabbitMQ.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventAttribute : Attribute
    {
        public string Value { get; set; }

        public EventAttribute(string value)
        {
            Value = value;
        }
    }
}

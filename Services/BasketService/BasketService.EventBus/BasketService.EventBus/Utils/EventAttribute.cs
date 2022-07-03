using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.EventBus.Utils
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

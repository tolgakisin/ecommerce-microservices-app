using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.EventBus.Models
{
    public interface IEvent
    {
        public string Data { get; set; }
        public string EventName { get; set; }
        public bool IsFinished { get; set; }
        public bool IsFailed { get; set; }
        public bool IsSync { get; set; }
    }
}

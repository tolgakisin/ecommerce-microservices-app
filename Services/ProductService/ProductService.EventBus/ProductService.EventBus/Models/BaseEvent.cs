
namespace ProductService.EventBus.Models
{
    public class BaseEvent : IEvent
    {
        public string Data { get; set; }
        public string EventName { get; set; }
        public bool IsFinished { get; set; }
        public bool IsFailed { get; set; }
        public bool IsSync { get; set; }
    }
}

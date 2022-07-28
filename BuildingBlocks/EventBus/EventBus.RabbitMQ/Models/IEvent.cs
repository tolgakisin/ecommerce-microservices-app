namespace EventBus.RabbitMQ.Models
{
    public interface IEvent
    {
        public string EventName { get; set; }
        public bool EventFinished { get; set; }
        public bool EventSync { get; set; }
        public int EventId { get; set; }
        public bool EventFailed { get; set; }
        public string EventErrorMessage { get; set; }
        public bool EventReverseStarted { get; set; }
    }
}

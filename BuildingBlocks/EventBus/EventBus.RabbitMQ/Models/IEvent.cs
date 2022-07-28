namespace EventBus.RabbitMQ.Models
{
    public interface IEvent
    {
        public string EventName { get; set; }
        public bool IsFinished { get; set; }
        public bool IsSync { get; set; }
        public int EventId { get; set; }
        public bool IsFailed { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsReverseStarted { get; set; }
    }
}

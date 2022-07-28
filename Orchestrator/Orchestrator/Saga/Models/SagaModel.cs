namespace Orchestrator.Saga.Models
{
    public class SagaModel
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

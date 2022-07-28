namespace Orchestrator.Saga.Models
{
    public class SagaModel
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

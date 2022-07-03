using Orchestrator.Data.Entities.Base;

namespace Orchestrator.Data.Entities
{
    public class EventProcess : BaseEntity
    {
        public string Name { get; set; }
        public int? PreviousId { get; set; }
    }
}

using Orchestrator.Common.Enums;
using Orchestrator.Data.Entities.Base;
using System;

namespace Orchestrator.Data.Entities
{
    public class EventLog : BaseEntity
    {
        public int EventId { get; set; }
        public string Data { get; set; }
        public DateTime ExecutionDate { get; set; }
        public EventState State { get; set; }
        public Guid UserId { get; set; }
    }
}

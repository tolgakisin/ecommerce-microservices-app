using System;

namespace EventBus.Core
{
    public class EventNames
    {
        public const string Event1 = "Event1";
        public const string Event2 = "Event2";
        public const string Event3 = "Event3";
        public const string Event4 = "Event4";
        public const string Event5 = "Event5";
        public const string Event6 = "Event6";

        public const string OrchestratorGeneralEvent = "orchestrator-general-event";

        public const string OrderCreatedEvent = "OrderCreatedEvent";
        public const string OrderStartedEvent = "OrderStartedEvent";

        public const string PaymentSuccessEvent = "PaymentSuccessEvent";
        public const string PaymentFailedEvent = "PaymentFailedEvent";
    }
}

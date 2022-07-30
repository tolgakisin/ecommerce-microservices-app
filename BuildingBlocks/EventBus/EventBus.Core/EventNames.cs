using System;

namespace EventBus.Core
{
    public class EventNames
    {
        public class Orchestrator
        {
            public const string OrchestratorGeneralEvent = "orchestrator-general-event";
        }

        public class Order
        {
            public const string OrderCreatedEvent = "OrderCreatedEvent";
            public const string OrderStartedEvent = "OrderStartedEvent";
            public const string OrderSubmittedEvent = "OrderSubmittedEvent";
        }

        public class Payment
        {
            public const string PaymentSuccessEvent = "PaymentSuccessEvent";
            public const string PaymentFailedEvent = "PaymentFailedEvent";
        }
    }
}

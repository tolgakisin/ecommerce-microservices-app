using System;

namespace EventBus.Core
{
    public sealed class EventNames
    {
        public sealed class Orchestrator
        {
            public const string OrchestratorGeneralEvent = "orchestrator-general-event";
        }

        public sealed class Order
        {
            public const string OrderCreatedEvent = "OrderCreatedEvent";
            public const string OrderStartedEvent = "OrderStartedEvent";
            public const string OrderSubmittedEvent = "OrderSubmittedEvent";
        }

        public sealed class Payment
        {
            public const string PaymentSuccessEvent = "PaymentSuccessEvent";
            public const string PaymentFailedEvent = "PaymentFailedEvent";
        }
    }
}

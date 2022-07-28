using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using OrderService.API.EventModels;
using System;

namespace OrderService.API.IntegrationEvents.Events
{
    [Event(EventNames.Order.OrderCreatedEvent)]
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent()
        {

        }

        public OrderCreatedEvent(Guid userId, CustomerBasket customerBasket, CustomerAddress customerAddress, CustomerPayment customerPayment)
        {
            UserId = userId;
            CustomerBasket = customerBasket;
            CustomerAddress = customerAddress;
            CustomerPayment = customerPayment;
        }

        public Guid UserId { get; set; }
        public CustomerBasket CustomerBasket { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public CustomerPayment CustomerPayment { get; set; }
    }
}

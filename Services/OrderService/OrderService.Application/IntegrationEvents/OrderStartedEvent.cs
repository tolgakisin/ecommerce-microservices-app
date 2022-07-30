using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using OrderService.Domain.Models;
using System;

namespace OrderService.Application.IntegrationEvents
{
    [Event(EventNames.Order.OrderStartedEvent)]
    public class OrderStartedEvent : BaseEvent
    {
        public OrderStartedEvent(Guid userId, CustomerBasket customerBasket, CustomerAddress customerAddress, CustomerPayment customerPayment)
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

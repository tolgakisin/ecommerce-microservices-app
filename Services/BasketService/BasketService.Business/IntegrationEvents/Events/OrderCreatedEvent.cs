using BasketService.Data.Contracts.Entities.Basket;
using BasketService.Data.Contracts.FakeEntities;
using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using System;

namespace BasketService.Business.IntegrationEvents.Events
{
    [Event(EventNames.OrderCreatedEvent)]
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

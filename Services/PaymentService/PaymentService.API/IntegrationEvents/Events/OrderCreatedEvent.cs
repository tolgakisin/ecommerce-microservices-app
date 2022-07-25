using EventBus.Core;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.Utils;
using PaymentService.API.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.Events
{
    [Event(EventNames.BasketService.OrderCreatedEvent)]
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent()
        {

        }

        public OrderCreatedEvent(string userId, CustomerBasket customerBasket, CustomerAddress customerAddress, CustomerPayment customerPayment)
        {
            UserId = userId;
            CustomerBasket = customerBasket;
            CustomerAddress = customerAddress;
            CustomerPayment = customerPayment;
        }

        public string UserId { get; set; }
        public CustomerBasket CustomerBasket { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public CustomerPayment CustomerPayment { get; set; }
    }
}

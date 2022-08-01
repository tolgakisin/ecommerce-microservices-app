using MediatR;
using OrderService.Application.Dtos;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public OrderAddressDto OrderAddress { get; set; }
        public OrderPaymentDto OrderPayment { get; set; }


        public CreateOrderCommand(Guid userId, CustomerBasket customerBasket, CustomerAddress customerAddress, CustomerPayment customerPayment)
        {
            UserId = userId;

            OrderItems = customerBasket.BasketItems.Select(x => new OrderItemDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            });

            OrderAddress = new OrderAddressDto
            {
                City = customerAddress.City,
                Country = customerAddress.Country,
                State = customerAddress.State,
                Street = customerAddress.Street,
                ZipCode = customerAddress.ZipCode
            };

            OrderPayment = new OrderPaymentDto
            {
                CardHolderName = customerPayment.CardHolderName,
                CardNumber = customerPayment.CardNumber,
                CardSecurityNumber = customerPayment.CardSecurityNumber,
                CardTypeId = customerPayment.CardTypeId,
                ExpirationDate = customerPayment.ExpirationDate
            };
        }
    }
}

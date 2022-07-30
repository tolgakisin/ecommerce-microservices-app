using MediatR;
using System;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.PaymentSuccessCommand
{
    public class PaymentSuccessCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }

        public PaymentSuccessCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

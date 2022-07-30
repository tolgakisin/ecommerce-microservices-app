using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.PaymentFailedCommand
{
    public class PaymentFailedCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }

        public PaymentFailedCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

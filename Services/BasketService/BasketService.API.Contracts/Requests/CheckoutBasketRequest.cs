using BasketService.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Requests
{
    public class CheckoutBasketRequest
    {
        public CustomerAddressModel CustomerAddress { get; set; }
        public CustomerPaymentModel CustomerPayment { get; set; }
    }
}

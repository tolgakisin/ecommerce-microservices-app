using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Models
{
    public class CustomerPaymentModel
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
    }
}

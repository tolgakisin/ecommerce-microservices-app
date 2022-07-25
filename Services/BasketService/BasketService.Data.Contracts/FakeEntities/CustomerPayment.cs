using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Data.Contracts.FakeEntities
{
    public class CustomerPayment
    {
        public CustomerPayment(string cardNumber, string cardHolderName, DateTime expirationDate, string cardSecurityNumber, int cardTypeId)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
        }

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
    }
}

using OrderService.Domain.Common;
using System;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Payment : BaseEntity
    {
        public Payment(string cardNumber, string cardHolderName, DateTime expirationDate, string cardSecurityNumber, int cardTypeId)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;

            if (string.IsNullOrWhiteSpace(cardNumber)) throw new Exception("Card Number is empty.");
            if (string.IsNullOrWhiteSpace(cardHolderName)) throw new Exception("Card Holder Name is empty.");
            if (string.IsNullOrWhiteSpace(cardSecurityNumber)) throw new Exception("Card Security Number is empty.");

            if (expirationDate < DateTime.Now)
                throw new Exception("Invalid expiration date.");
        }

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
        public CardType CardType { get; private set; }
    }
}

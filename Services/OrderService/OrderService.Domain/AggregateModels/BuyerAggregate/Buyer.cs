using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Domain.AggregateModels.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public Guid BuyerId { get; set; }

        private List<Payment> _payments { get; set; }
        public IEnumerable<Payment> Payments => _payments.AsReadOnly();

        protected Buyer()
        {
            _payments = new List<Payment>();
        }

        public Payment SavePaymentMethod(string cardNumber, string cardHolderName, DateTime expirationDate, string cardSecurityNumber, int cardTypeId)
        {
            var existPayment = Payments.FirstOrDefault(x => x.CardNumber == cardNumber && x.CardTypeId == cardTypeId && x.ExpirationDate == expirationDate);

            if (existPayment != null)
            {
                return existPayment;
            }

            var payment = new Payment(cardNumber, cardHolderName, expirationDate, cardSecurityNumber, cardTypeId);
            _payments.Add(payment);

            return payment;
        }
    }
}

using System;

namespace OrderService.Application.Dtos
{
    public class OrderPaymentDto
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public record Address
    {
        public Address()
        {

        }

        public Address(string city, string street, string state, string country, string zipCode, Guid buyerId)
        {
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
            BuyerId = buyerId;
        }

        public string City { get; private set; }
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public Guid BuyerId { get; set; }
    }
}

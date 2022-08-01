using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Address : BaseEntity
    {
        public Address()
        {

        }

        public Address(string city, string street, string state, string country, string zipCode)
        {
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string City { get; private set; }
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}

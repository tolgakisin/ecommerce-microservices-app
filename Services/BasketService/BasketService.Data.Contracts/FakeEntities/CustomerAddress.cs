using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Data.Contracts.FakeEntities
{
    public class CustomerAddress
    {
        public CustomerAddress(string city, string street, string state, string country, string zipCode)
        {
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}

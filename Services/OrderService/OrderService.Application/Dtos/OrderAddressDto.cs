namespace OrderService.Application.Dtos
{
    public class OrderAddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}

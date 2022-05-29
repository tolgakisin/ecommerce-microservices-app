using ProductService.Query.QueryModels;
using System;

namespace ProductService.Query.Queries.GetAllProducts
{
    public class GetAllProductsQueryResponse : GetAllProductsModel
    {
        public GetAllProductsQueryResponse()
        {

        }

        public GetAllProductsQueryResponse(Guid id, string name, int quantity, decimal price, string description)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = description;
        }
    }
}

using ProductService.Query.QueryModels;
using System;

namespace ProductService.Query.Queries.GetProductById
{
    public class GetByIdQueryResponse : GetByIdModel
    {
        public GetByIdQueryResponse()
        {

        }

        public GetByIdQueryResponse(Guid id, string name, int quantity, decimal price, string description)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Description = description;
        }
    }
}

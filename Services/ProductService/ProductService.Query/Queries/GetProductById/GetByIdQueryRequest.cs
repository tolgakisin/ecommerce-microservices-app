using System;
using MediatR;

namespace ProductService.Query.Queries.GetProductById
{
    public class GetByIdQueryRequest : IRequest<GetByIdQueryResponse>
    {
        public GetByIdQueryRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}

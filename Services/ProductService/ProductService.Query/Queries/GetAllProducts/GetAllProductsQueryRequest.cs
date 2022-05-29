using MediatR;
using System.Collections.Generic;

namespace ProductService.Query.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<IEnumerable<GetAllProductsQueryResponse>>
    {
    }
}

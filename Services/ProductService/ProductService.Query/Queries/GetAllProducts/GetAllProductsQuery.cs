using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Query.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Query.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequestHandler<GetAllProductsQueryRequest, IEnumerable<GetAllProductsQueryResponse>>
    {
        private readonly ProductReadDbContext _productReadDbContext;

        public GetAllProductsQuery(ProductReadDbContext productReadDbContext)
        {
            _productReadDbContext = productReadDbContext;
        }

        public async Task<IEnumerable<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _productReadDbContext.Products.Select(x => new GetAllProductsQueryResponse(x.Id, x.Name, x.Quantity, x.Price, x.Description)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}

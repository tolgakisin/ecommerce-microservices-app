using MediatR;
using ProductService.Query.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Query.Queries.GetProductById
{
    public class GetByIdQuery : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly ProductReadDbContext _productReadDbContext;

        public GetByIdQuery(ProductReadDbContext productReadDbContext)
        {
            _productReadDbContext = productReadDbContext;
        }

        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productReadDbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            return new GetByIdQueryResponse(entity.Id, entity.Name, entity.Quantity, entity.Price, entity.Description);
        }
    }
}

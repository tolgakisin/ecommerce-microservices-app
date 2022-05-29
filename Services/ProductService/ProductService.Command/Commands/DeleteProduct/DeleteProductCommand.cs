using MassTransit;
using MediatR;
using ProductService.Command.Data.Common;
using ProductService.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Command.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteProductCommand(ProductDbContext productDbContext, IPublishEndpoint publishEndpoint)
        {
            _productDbContext = productDbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _productDbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (entity == null) throw new Exception("Product is not found.");

            try
            {
                _productDbContext.Products.Remove(entity);

                await _publishEndpoint.Publish(new ProductDeletedEvent { Id = entity.Id });
                //TODO Add Transaction between command and query.

                _ = await _productDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return new DeleteProductCommandResponse(entity.Name);
        }

    }
}

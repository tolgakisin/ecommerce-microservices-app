using MassTransit;
using MediatR;
using ProductService.Command.Data.Common;
using ProductService.Core.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Command.Commands.CreateProduct
{
    public class CreateProductCommand : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateProductCommand(ProductDbContext productDbContext, IPublishEndpoint publishEndpoint)
        {
            _productDbContext = productDbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = (await _productDbContext.Products.AddAsync(new Data.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                Description = request.Description
            }, cancellationToken)).Entity;

            try
            {
                await _publishEndpoint.Publish<ProductCreatedEvent>(new ProductCreatedEvent
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Quantity = entity.Quantity,
                    Price = entity.Price,
                    Description = entity.Description
                });
                //TODO: Add Transaction between command and query.

                _ = await _productDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }

            return new CreateProductCommandResponse(entity.Id, entity.Name);
        }
    }
}

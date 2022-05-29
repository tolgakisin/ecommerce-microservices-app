using MassTransit;
using ProductService.Core.Events;
using ProductService.Query.Data.Common;
using ProductService.Query.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Query.Consumers
{
    public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ProductReadDbContext _productReadDbContext;

        public ProductCreatedEventConsumer(ProductReadDbContext productReadDbContext)
        {
            _productReadDbContext = productReadDbContext;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            try
            {
                var _ = await _productReadDbContext.Products.AddAsync(new Product
                {
                    Id = context.Message.Id,
                    Name = context.Message.Name,
                    Quantity = context.Message.Quantity,
                    Price = context.Message.Price,
                    Description = context.Message.Description
                });

                await _productReadDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

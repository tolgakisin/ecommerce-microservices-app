using MassTransit;
using ProductService.Core.Events;
using ProductService.Query.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Query.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly ProductReadDbContext _productReadDbContext;

        public ProductDeletedEventConsumer(ProductReadDbContext productReadDbContext)
        {
            _productReadDbContext = productReadDbContext;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var entity = await _productReadDbContext.Products.FindAsync(context.Message.Id);

            try
            {
                _productReadDbContext.Products.Remove(entity);

                await _productReadDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

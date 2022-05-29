using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Query.Consumers
{
    public class ProductDeletedEventConsumerDefinition : ConsumerDefinition<ProductDeletedEventConsumer>
    {
        public ProductDeletedEventConsumerDefinition()
        {
            EndpointName = "product-service-product-deleted";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ProductDeletedEventConsumer> consumerConfigurator)
        {
            base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        }
    }
}

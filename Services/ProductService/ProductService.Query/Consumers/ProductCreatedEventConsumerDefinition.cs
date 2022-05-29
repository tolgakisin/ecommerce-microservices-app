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
    public class ProductCreatedEventConsumerDefinition : ConsumerDefinition<ProductCreatedEventConsumer>
    {
        public ProductCreatedEventConsumerDefinition()
        {
            EndpointName = "product-service-product-created";
            ConcurrentMessageLimit = 1;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ProductCreatedEventConsumer> consumerConfigurator)
        {
            base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        }
    }
}

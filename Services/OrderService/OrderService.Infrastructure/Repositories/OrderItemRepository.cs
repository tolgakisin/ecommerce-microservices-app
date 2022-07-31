﻿using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        public OrderItemRepository(OrderDbContext context) : base(context)
        {

        }
    }
}

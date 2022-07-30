using OrderService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderStatus : CustomEnumeration
    {
        private OrderStatus(int id, string name) : base(id, name)
        {

        }

        public static readonly OrderStatus Pending = new(1, nameof(Pending));
        public static readonly OrderStatus StockConfirmed = new(2, nameof(StockConfirmed));
        public static readonly OrderStatus Paid = new(3, nameof(Paid));
        public static readonly OrderStatus Shipped = new(4, nameof(Shipped));
        public static readonly OrderStatus Submitted = new(5, nameof(Submitted));
        public static readonly OrderStatus Cancelled = new(6, nameof(Cancelled));
    }
}

using Microsoft.EntityFrameworkCore;
using OrderService.API.Data.Entities;

namespace OrderService.API.Data.Common
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}

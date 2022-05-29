using Microsoft.EntityFrameworkCore;
using ProductService.Command.Data.Entities;

namespace ProductService.Command.Data.Common
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}

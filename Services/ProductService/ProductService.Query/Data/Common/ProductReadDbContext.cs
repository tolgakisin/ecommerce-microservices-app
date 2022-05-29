using Microsoft.EntityFrameworkCore;
using ProductService.Query.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Query.Data.Common
{
    public class ProductReadDbContext : DbContext
    {
        public ProductReadDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}

using Backend.Modules.ProductCatalog.Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.ProductCatalog.Infrastructure.Persistence
{
    public class ProductCatalogDbContext : DbContext
    {
        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }

}
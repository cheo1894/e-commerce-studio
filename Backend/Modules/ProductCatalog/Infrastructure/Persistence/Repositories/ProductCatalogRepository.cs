using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.ProductCatalog.Infrastructure.Persistence.Repositories
{

    public class ProductCatalogRepository : IProductCatalogRepository
    {

        private readonly ProductCatalogDbContext _context;

        public ProductCatalogRepository(ProductCatalogDbContext context)
        {

            _context = context;

        }
        public async Task<IEnumerable<Product>> Get() => await _context.Products.Include(p => p.Category).Where(p => p.Active == true).ToListAsync();
        public async Task<Product> GetById(int id) => await _context.Products.FindAsync(id);
        public async Task Add(Product product) => await _context.Products.AddAsync(product);
        public void Update(Product product)
        {
            _context.Products.Attach(product);
            _context.Products.Update(product).State = EntityState.Modified;
        }
        public async Task Save() => await _context.SaveChangesAsync();

    }

}
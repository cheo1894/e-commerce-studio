using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.ProductCatalog.Infrastructure.Persistence.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {

        private readonly ProductCatalogDbContext _context;
        public CategoryRepository(ProductCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Get()
       => await _context.Categories.Where(c => c.Active == true).ToListAsync();

        public async Task<Category> GetById(int id)
      => await _context.Categories.FindAsync(id);
        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);

        }
        public void Update(Category category)
        {
            _context.Categories.Attach(category);
            _context.Categories.Update(category).State = EntityState.Modified;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
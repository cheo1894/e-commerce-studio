using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Backend.Modules.ProductCatalog.Application.Services
{
    public class CategoryServices : ICategoryService
    {


        private readonly ProductCatalogDbContext _context;

        public CategoryServices(ProductCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> Get()
        {
            var categories = await _context.Categories.Where(c => c.Active == true).ToListAsync();
            return categories.Select(c => new CategoryDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Active = c.Active,
            }).ToList();
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null || category.Active == false) return null;


            var categoryDto = new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Active = category.Active
            };
            return categoryDto;
        }
        public async Task<CategoryDto> Add(CategoryInsertDto dto)
        {
            var category = new Category()
            {
                CategoryName = dto.CategoryName,
                Active = true
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            var categoryDto = new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Active = category.Active
            };
            return categoryDto;
        }

        public async Task<CategoryDto> Update(int id, CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null || category.Active == false) return null;
            category.CategoryName = dto.CategoryName;
            category.Active = dto.Active;
            _context.Categories.Attach(category);
            _context.Categories.Update(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var categoryDto = new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Active = category.Active
            };
            return categoryDto;
        }
        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null || category.Active == false) return false;
            category.Active = false;
            _context.Categories.Attach(category);
            _context.Categories.Update(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Domain.Interfaces;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Backend.Modules.ProductCatalog.Application.Services
{
    public class CategoryServices : ICategoryService
    {


        private readonly ProductCatalogDbContext _context;
        private readonly ICategoryRepository _repository;

        public CategoryServices(ProductCatalogDbContext context, ICategoryRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> Get()
        {
            var categories = await _repository.Get();
            return categories.Select(c => new CategoryDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Active = c.Active,
            }).ToList();
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _repository.GetById(id);

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
            await _repository.Add(category);
            await _repository.Save();

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
            var category = await _repository.GetById(id);
            if (category == null || category.Active == false) return null;
            category.CategoryName = dto.CategoryName;
            category.Active = dto.Active;
            _repository.Update(category);
            await _repository.Save();
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
            _repository.Update(category);
            await _repository.Save();

            return true;
        }


    }
}
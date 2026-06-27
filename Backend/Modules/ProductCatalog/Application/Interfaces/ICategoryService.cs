using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Domain.entities;

namespace Backend.Modules.ProductCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> Get();
        Task<CategoryDto> GetById(int id);
        Task<CategoryDto> Add(CategoryInsertDto dto);
        Task<CategoryDto> Update(int id, CategoryUpdateDto dto);
        Task<bool> Delete(int id);
    }
}
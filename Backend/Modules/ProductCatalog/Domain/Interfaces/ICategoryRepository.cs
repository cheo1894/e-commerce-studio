using Backend.Modules.ProductCatalog.Domain.entities;

namespace Backend.Modules.ProductCatalog.Domain.Interfaces
{
    public interface ICategoryRepository
    {


        Task<IEnumerable<Category>> Get();
        Task<Category> GetById(int id);
        Task Add(Category category);
        void Update(Category category);
        Task Save();
    }
}
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Domain.entities;

namespace Backend.Modules.ProductCatalog.Domain.Interfaces
{
    public interface IProductCatalogRepository
    {
        Task<IEnumerable<Product>> Get();
        Task<Product> GetById(int id);
        Task Add(Product product);
        void Update(Product product);
        Task Save();
    }
}
using System.ComponentModel;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Domain.entities;

namespace Backend.Modules.ProductCatalog.Application.Interfaces
{


    public interface IProductCatalogService
    {
        Task<IEnumerable<ProductDto>> Get();
        Task<ProductDto> GetById(int id);
        Task<ProductDto> Add(ProductInsertDto dto, int userId);
        Task<ProductDto> Update(int id, ProductUpdateDto dto);

        Task<bool> Delete(int id);
    }


}
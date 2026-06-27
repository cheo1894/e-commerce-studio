using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Domain.Interfaces;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Modules.ProductCatalog.Application.Services
{
    public class ProductCatalogService : IProductCatalogService
    {

        private readonly ProductCatalogDbContext _context;
        private readonly IProductCatalogRepository _repo;
        private IuserNameProvider _userNameProvider;
        public ProductCatalogService(ProductCatalogDbContext context, IuserNameProvider userNameProvider, IProductCatalogRepository repo)
        {
            _context = context;
            _repo = repo;
            _userNameProvider = userNameProvider;
        }


        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _repo.Get();

            return await Task.WhenAll(products.Select(async (p) =>
            {
                var userName = await _userNameProvider.GetUserNameByIdAsync(p.CreatedById);
                return new ProductDto()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductDescription = p.ProductDescription,
                    Quantity = p.Quantity,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId,
                    CreatedById = p.CreatedById,
                    Category = p.Category.CategoryName,
                    CreatedByName = userName
                };
            }));
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _repo.GetById(id);

            if (product == null || product.Active == false) return null;


            var productoDto = new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CreatedById = product.CategoryId

            };

            return productoDto;
        }
        public async Task<ProductDto> Add(ProductInsertDto dto, int userId)
        {

            var product = new Product()
            {
                ProductName = dto.ProductName,
                ProductPrice = dto.ProductPrice,
                ProductDescription = dto.ProductDescription,
                Quantity = dto.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = dto.ImageUrl,
                Active = true,
                CategoryId = dto.CategoryId,
                CreatedById = userId
            };
            await _repo.Add(product);

            await _repo.Save();


            var productoDto = new ProductDto()
            {

                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CreatedById = product.CategoryId

            };


            return productoDto;
        }

        public async Task<ProductDto> Update(int id, ProductUpdateDto dto)
        {
            var product = await _repo.GetById(id);

            if (product == null || product.Active == false)
            {
                return null;
            }
            product.ProductName = dto.ProductName;
            product.ProductPrice = dto.ProductPrice;
            product.ProductDescription = dto.ProductDescription;
            product.Quantity = dto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;
            product.CreatedById = dto.CategoryId;


            _repo.Update(product);
            await _repo.Save();
            var productoDto = new ProductDto()
            {


                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CreatedById = product.CategoryId

            };
            return productoDto;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _repo.GetById(id);

            if (product == null || product.Active == false)
            {
                return false;
            }
            product.Active = false;
            product.UpdatedAt = new DateTime();
            _repo.Update(product);
            await _repo.Save();
            return true;
        }
    }
}
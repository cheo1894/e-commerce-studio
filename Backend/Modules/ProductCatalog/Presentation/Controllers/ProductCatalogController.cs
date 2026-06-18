using System.Security.Claims;
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Backend.Modules.ProductCatalog.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]


    public class ProductCatalogController : ControllerBase
    {
        private ProductCatalogDbContext _context;
        private IuserNameProvider _userNameProvider;


        public ProductCatalogController(ProductCatalogDbContext context, IuserNameProvider userNameProvider)
        {
            _context = context;
            _userNameProvider = userNameProvider;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {

            var products = await _context.Products.Include(p => p.Category).Where(p => p.Active == true).ToListAsync();

            //Task.WhenAll.... espera todas las tareas y devuelve una lista resuelta... si, es necesario el "Task" porque es una tarea asíncrona
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


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.Active == false)
            {
                return NotFound();
            }

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

            return Ok(productoDto);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ProductDto>> Add(ProductInsertDto dto)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

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

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();


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


            return CreatedAtAction(nameof(GetById), new { id = productoDto.ProductId }, productoDto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id, ProductUpdateDto dto)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null || product.Active == false)
            {
                return NotFound();
            }
            product.ProductName = dto.ProductName;
            product.ProductPrice = dto.ProductPrice;
            product.ProductDescription = dto.ProductDescription;
            product.Quantity = dto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;
            product.CreatedById = dto.CategoryId;


            _context.Products.Attach(product);
            _context.Products.Update(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
            return Ok(productoDto);
        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.Active == false)
            {
                return NotFound();
            }
            product.Active = false;
            product.UpdatedAt = new DateTime();
            _context.Products.Attach(product);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
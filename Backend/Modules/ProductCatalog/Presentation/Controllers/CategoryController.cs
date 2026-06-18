using Backend.Modules.ProductCatalog.Application.DTOs;
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
    public class CategoryController : ControllerBase
    {


        private ProductCatalogDbContext _context;

        public CategoryController(ProductCatalogDbContext context)
        {
            _context = context;
        }


        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {

            var category = await _context.Categories.FindAsync(id);

            if (category == null || category.Active == false)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Active = category.Active
            };
            return Ok(categoryDto);
        }



        [HttpPost("add")]
        public async Task<ActionResult<CategoryDto>> Add(CategoryInsertDto dto)
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
            return CreatedAtAction(
                nameof(GetById),
                new { id = categoryDto.CategoryId },
                categoryDto
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, CategoryUpdateDto dto)
        {

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
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
            return Ok(categoryDto);
        }


        [HttpPut("delete/{id}")]
        public async Task<ActionResult<CategoryDto>> SoftDelete(int id)
        {

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Active = false;

            _context.Categories.Attach(category);
            _context.Categories.Update(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Active = category.Active
            };
        }


    }
}
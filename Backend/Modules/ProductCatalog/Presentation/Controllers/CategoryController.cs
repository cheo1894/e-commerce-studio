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
    public class CategoryController : ControllerBase
    {



        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {

            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get() => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _service.GetById(id);
            return category != null ? Ok(category) : NotFound();
        }



        [HttpPost("add")]
        public async Task<ActionResult<CategoryDto>> Add(CategoryInsertDto dto)
        {
            var res = await _service.Add(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = res.CategoryId },
                res
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, CategoryUpdateDto dto)
        {
            var category = await _service.Update(id, dto);
            return category != null ? Ok(category) : NotFound();
        }


        [HttpPut("delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var category = await _service.Delete(id);
            var message = new { message = "Categoría borada exitosamente" };
            return category == true ? Ok(message) : NotFound();
        }


    }
}
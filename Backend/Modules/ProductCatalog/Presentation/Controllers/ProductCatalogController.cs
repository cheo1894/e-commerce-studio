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

        private readonly IProductCatalogService _service;


        public ProductCatalogController(ProductCatalogDbContext context, IuserNameProvider userNameProvider, IProductCatalogService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get() => await _service.Get();



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _service.GetById(id);

            return product == null ? NotFound() : Ok(product);

        }

        [HttpPost("add")]
        public async Task<ActionResult<ProductDto>> Add(ProductInsertDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var res = await _service.Add(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = res.ProductId }, res);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id, ProductUpdateDto dto)
        {
            var res = await _service.Update(id, dto);

            return res != null ? Ok(res) : NotFound();

        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _service.Delete(id);
            var message = new { message = "Producto eliminado satisfactoriamente" };
            return res == true ? Ok(message) : NotFound();
        }

    }
}
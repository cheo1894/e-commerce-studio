using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Modules.Auth.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService _service;
        public RolesController(IRoleService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<RoleDto>> GetRoles() => await _service.Get();




        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleByID(int id)
        {
            var res = await _service.GetById(id);
            return res != null ? Ok(res) : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<RoleDto>> AddRole(RoleInsertDto dto)
        {
            var res = await _service.Add(dto);
            return CreatedAtAction(
                nameof(GetRoleByID),
                new { id = res.RoleId },
                res
            );
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Update(int id, RoleUpdateDto dto)
        {
            var res = await _service.Update(id, dto);
            return res != null ? Ok(res) : NotFound();
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _service.Delete(id);
            var message = new { message = "Eliminado satisfactoriamente" };
            return res == true ? Ok(message) : NotFound();
        }
    }
}
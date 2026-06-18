using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Backend.Modules.Auth.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private AuthDbContext _context;
        public RolesController(AuthDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var res = await _context.Roles.ToListAsync();

            return res.Select(r => new RoleDto()
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,

            });

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoloByID(int id)
        {
            var res = await _context.Roles.FindAsync(id);

            if (res == null)
            {
                return NotFound();
            }

            var roleDto = new RoleDto()
            {
                RoleId = res.RoleId,
                RoleName = res.RoleName,
            };
            return roleDto;
        }


        [HttpPost]
        public async Task<ActionResult<RoleDto>> AddRole(RoleInsertDto dto)
        {
            var role = new Role()
            {
                RoleName = dto.RoleName,
                Active = true
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            var roleDto = new RoleDto()
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
            return CreatedAtAction(
                nameof(GetRoloByID),
                new { id = role.RoleId },

                roleDto
            );
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Update(int id, RoleUpdateDto dto)
        {
            var res = await _context.Roles.FindAsync(id);

            res.RoleName = dto.RoleName;


            _context.Roles.Attach(res);
            _context.Roles.Update(res).State = EntityState.Modified;

            var roleDto = new RoleDto()
            {
                RoleId = res.RoleId,
                RoleName = res.RoleName
            };
            return Ok(roleDto);


        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<RoleDto>> Delete(int id)
        {

            var res = await _context.Roles.FindAsync(id);


            res.Active = false;

            _context.Roles.Attach(res);
            _context.Roles.Update(res).State = EntityState.Modified;


            await _context.SaveChangesAsync();



            var roleDto = new RoleDto()
            {
                RoleId = res.RoleId,
                RoleName = res.RoleName
            };

            return Ok(roleDto);

        }
    }
}
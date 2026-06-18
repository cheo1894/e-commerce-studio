using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Auth.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers() => await _service.Get();


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _service.GetById(id);

            return user != null ? Ok(user) : NotFound();
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserInsertDto>> Add(UserInsertDto dto)
        {

            //Agregar validaciones con FluentValidation
            //Agregar validación de existencia
            var user = await _service.Add(dto);


            return CreatedAtAction(
                nameof(GetUserById),
                new { id = user.UserId },
                user
            );
        }



    }
}
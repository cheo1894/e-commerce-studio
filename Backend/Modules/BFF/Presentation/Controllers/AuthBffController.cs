using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.BFF.Application.DTOs;
using Backend.Modules.BFF.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.BFF.Presentation.Controllers
{
    [Route("/api/bff/auth")]
    [ApiController]
    public class AuthBffController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenCache _cache;
        public AuthBffController(IAuthService authService, ITokenCache cache)
        {
            _authService = authService;
            _cache = cache;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto request)
        {

            var result = await _authService.Login(request);

            if (result == null) Unauthorized();

            var sessionId = result.SessionId;
            await _cache.SetAsync(sessionId, new TokenDto() { AccessToken = result!.Token });


            Response.Cookies.Append("session_id", sessionId, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });


            return Ok(new { message = "Login exitoso" });


        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessionId = Request.Cookies["session_id"];
            Console.WriteLine($"ESTE ES EL SESSION_ID > {sessionId}");
            if (!string.IsNullOrEmpty(sessionId))
            {
                // Opcional: revocar refresh token llamando a Auth
                await _authService.Logout(sessionId);
                await _cache.RemoveAsync(sessionId);
                Response.Cookies.Delete("session_id");
            }
            return Ok("Sesión terminada");
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var sessionId = Request.Cookies["session_id"];
            if (string.IsNullOrEmpty(sessionId)) return Unauthorized();

            var tokenDto = await _cache.GetAsync(sessionId);
            if (tokenDto == null) return Unauthorized();

            var user = await _authService.FindUserBySessionId(sessionId);

            if (user == null) return Unauthorized();

            // Aquí podrías decodificar el JWT o llamar a Auth para obtener perfil
            return Ok(user);
        }
    }
}
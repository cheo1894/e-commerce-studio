using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Application.Services;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Modules.Auth.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private AuthDbContext _context;
        private readonly IAuthService _service;

        private readonly ITokenService _jwtTokenService;

        public AuthController(AuthDbContext context, IAuthService service, ITokenService jwtTokenService)
        {
            _context = context;
            _service = service;

            _jwtTokenService = _jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginUserDto dto)
        {
            var user = await _service.Login(dto);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO request)
        {

            if (string.IsNullOrWhiteSpace(request.RefreshToken) || string.IsNullOrWhiteSpace(request.SessionId))
                return Unauthorized("Faltan datos de refresh token");
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.SessionId == request.SessionId
            && rt.ExpiresAtUtc > DateTime.UtcNow);

            if (storedToken == null) return Unauthorized("Sesión inválida o expirada");

            bool valid = _jwtTokenService.verifyRefreshToken(request.RefreshToken, storedToken.TokenHash);

            if (!valid) return Unauthorized("Refresh token inválido");

            var user = await _context.Users.FindAsync(storedToken.UserId);

            if (user == null) return Unauthorized("Usuario no encontrado");

            var newAccessToken = _jwtTokenService.GenerateToken(user.UserId, user.UserName, user.RoleId);

            storedToken.RevokedAtUtc = DateTime.UtcNow;
            _context.RefreshTokens.Attach(storedToken);
            _context.RefreshTokens.Update(storedToken).State = EntityState.Modified;

            var newRefreshToke = _jwtTokenService.GenerateRefreshToken();
            var newhashed = _jwtTokenService.HashRefreshToken(newAccessToken);
            var newSessionId = Guid.NewGuid().ToString();

            var newRefreshEntity = new RefreshToken()
            {
                UserId = user.UserId,
                SessionId = newSessionId,
                TokenHash = newhashed,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(1),
                RevokedAtUtc = null

            };

            await _context.RefreshTokens.AddAsync(newRefreshEntity);
            await _context.SaveChangesAsync();


            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToke,
                sessionId = newSessionId,

            });

        }
    }
}
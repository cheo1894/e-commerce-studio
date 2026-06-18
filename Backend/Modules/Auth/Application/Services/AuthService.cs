using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Interfaces;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Backend.Modules.ProductCatalog.Application.DTOs;
using Backend.Modules.ProductCatalog.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;

namespace Backend.Modules.Auth.Application.Services
{
    public class AuthService : IAuthService
    {


        private AuthDbContext _context;

        private IAuthRepository _repository;

        private readonly ITokenService _jwtTokenService;

        public AuthService(AuthDbContext context, IAuthRepository repository, ITokenService jwtTokenService)
        {
            _context = context;
            _repository = repository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDTO> Login(LoginUserDto dto)
        {
            var user = await _repository.FindUser(dto);

            if (user == null || user.password != dto.password)
            {
                return null;
            }


            //Generate Token
            var token = _jwtTokenService.GenerateToken(user.UserId, user.UserName, user.RoleId);

            //Generate Refresh Token 
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            //Hash Refresh Token
            var hashed = _jwtTokenService.HashRefreshToken(refreshToken);

            var refreshTokenEntity = new RefreshToken()
            {
                UserId = user.UserId,
                SessionId = Guid.NewGuid().ToString(),
                TokenHash = hashed,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(1),
                CreatedAtUtc = DateTime.UtcNow

            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new AuthResponseDTO()
            {
                UserName = user.UserName,
                RoleId = user.RoleId,
                UserId = user.UserId,
                Token = token,
                RefreshToken = refreshToken,
                SessionId = refreshTokenEntity.SessionId

            };
        }

        public async Task<bool> Logout(string sessionId)
        {
            var res = await _repository.FindToken(sessionId);
            res.RevokedAtUtc = DateTime.UtcNow;
            _context.RefreshTokens.Attach(res);
            _context.RefreshTokens.Update(res).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<UserDto> FindUserBySessionId(string sessionId)
        {
            var refreshToken = await _repository.FindToken(sessionId);

            if (refreshToken == null || refreshToken.RevokedAtUtc != null) return null;

            var user = await _repository.FindUserById(refreshToken.UserId);
            if (user == null) return null;

            return new UserDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleId = user.RoleId
            };

        }
    }
}
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Infrastructure.Services
{


    public class RefreshTokeService : IRefreshTokenService
    {


        private readonly AuthDbContext _context;
        private readonly ITokenService _jwtService;

        public RefreshTokeService(AuthDbContext context, ITokenService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<TokenRenewalResult?> RenewAccessTokenAsync(string sessionId)
        {
            var tokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.SessionId == sessionId && rt.RevokedAtUtc == null);


            if (tokenEntity == null || tokenEntity.ExpiresAtUtc < DateTime.UtcNow) return null;

            var user = await _context.Users.FindAsync(tokenEntity.UserId);

            if (user == null) return null;
            var newAccesstoken = _jwtService.GenerateToken(user.UserId, user.UserName, user.RoleId);

            var newRefreshToken = _jwtService.GenerateRefreshToken();
            tokenEntity.TokenHash = _jwtService.HashRefreshToken(newRefreshToken);
            tokenEntity.ExpiresAtUtc = DateTime.UtcNow.AddDays(1);
            _context.RefreshTokens.Attach(tokenEntity);
            _context.RefreshTokens.Update(tokenEntity).State = EntityState.Modified;

            return new TokenRenewalResult()
            {
                AccessToken = newAccesstoken
            };

        }
    }
}
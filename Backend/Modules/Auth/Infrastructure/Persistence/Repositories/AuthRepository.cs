using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Infrastructure.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private AuthDbContext _context;
        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindUser(LoginUserDto dto) => await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

        public async Task<User> FindUserById(int userId) => await _context.Users.FindAsync(userId);


        public Task<RefreshToken?> FindValidRefreshTokenAsync(string tokenHash)
        {
            throw new NotImplementedException();
        }
        public async Task<RefreshToken> FindToken(string sessionId) => await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.SessionId == sessionId && rt.RevokedAtUtc == null);


        public Task RemoveRefreshTokenAsync(int userId)
        {
            throw new NotImplementedException();
        }


        public Task RotateRefreshTokenAsync(int tokenId, string newHash, DateTime newExpiry)
        {
            throw new NotImplementedException();
        }

        public Task SaveRefreshTokenAsync(int userId, string tokenHash, DateTime expiresAtUtc)
        {
            throw new NotImplementedException();
        }


    }
}
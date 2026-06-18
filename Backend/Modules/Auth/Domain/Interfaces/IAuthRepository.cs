using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Application.DTOs;
using Backend.Modules.Auth.Domain.Entities;

namespace Backend.Modules.Auth.Domain.Interfaces
{
    public interface IAuthRepository
    {

        Task<User> FindUserById(int userId);
        Task<User> FindUser(LoginUserDto dto);
        Task SaveRefreshTokenAsync(int userId, string tokenHash, DateTime expiresAtUtc);
        Task<RefreshToken?> FindValidRefreshTokenAsync(string tokenHash);
        Task RemoveRefreshTokenAsync(int userId);

        Task RotateRefreshTokenAsync(int tokenId, string newHash, DateTime newExpiry);


        Task<RefreshToken> FindToken(string sessionId);

    }
}
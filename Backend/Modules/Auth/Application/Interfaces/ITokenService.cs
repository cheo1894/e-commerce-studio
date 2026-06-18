using System.Security.Claims;

namespace Backend.Modules.Auth.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string userName, int roleId);
        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

        string HashRefreshToken(string plainToken);

        bool verifyRefreshToken(string plainToken, string storedHashWithSalt);

        // byte[] CombineTokenAndSalt(string token, string salt);

        // byte[] CalculateSHA256WithInstance(byte[] combined);

    }
}
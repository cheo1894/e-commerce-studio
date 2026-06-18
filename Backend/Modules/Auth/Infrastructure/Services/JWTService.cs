using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Authl.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Modules.Auth.Infrastructure.Services
{

    public class JWTService : ITokenService
    {

        private readonly JWTSetting _jwtSetting;

        public JWTService(IOptions<JWTSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public string GenerateToken(int userId, string userName, int roleId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, roleId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                 audience: _jwtSetting.Audience,
                  claims: claims,
                   expires: DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
                   signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);

        }


        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _jwtSetting.Audience,
                ValidateIssuer = true,
                ValidIssuer = _jwtSetting.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return principal;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public string HashRefreshToken(string plainToken)
        {
            // 1. Generar salt con RandomNumberGenerator (instancia)
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            // 2. Combinar token + salt
            byte[] combined = CombineTokenAndSalt(plainToken, salt);

            // 3. Calcular SHA-256 con instancia
            byte[] hashBytes = CalculateSHA256WithInstance(combined);

            // 4. Devolver "salt:hash"    
            return $"{salt}:{Convert.ToBase64String(hashBytes)}";
        }

        public bool verifyRefreshToken(string plainToken, string storedHashWithSalt)
        {
            var parts = storedHashWithSalt.Split(":");
            if (parts.Length != 2) return false;
            string storedSalt = parts[0];
            string storedHash = parts[1];


            byte[] combined = CombineTokenAndSalt(plainToken, storedSalt);

            byte[] hashBytes = CalculateSHA256WithInstance(combined);

            string computedHash = Convert.ToBase64String(hashBytes);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(computedHash), Convert.FromBase64String(storedHash)
            );
        }

        //Para ser privados no deben estar en el contrato, son métodos privados de la clase
        private byte[] CombineTokenAndSalt(string token, string salt)
        {

            // return Encoding.UTF8.GetBytes(token)
            //    .Concat(Convert.FromBase64String(salt))
            //    .ToArray();

            //Esto es mas eficiente porque opera a bajo nivel;
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] combined = new byte[tokenBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(tokenBytes, 0, combined, 0, tokenBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combined, tokenBytes.Length, saltBytes.Length);
            return combined;
        }

        private byte[] CalculateSHA256WithInstance(byte[] combined)
        {
            byte[] hashBytes;
            using (var sha256 = SHA256.Create())
                hashBytes = sha256.ComputeHash(combined);

            return hashBytes;

        }

    }



}
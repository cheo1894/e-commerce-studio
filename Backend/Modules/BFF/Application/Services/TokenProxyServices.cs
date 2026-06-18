using System.IdentityModel.Tokens.Jwt;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.BFF.Application.DTOs;
using Backend.Modules.BFF.Application.Interfaces;

namespace Backend.Modules.BFF.Application.Services
{
    public class TokenProxyServices : ITokenProxyService
    {

        private readonly ITokenCache _cache;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenProxyServices(ITokenCache cache, IRefreshTokenService refreshTokenService)
        {
            _cache = cache;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<TokenDto?> GetValidTokenAsync(string sessionId)
        {
            // 1. Intentar obtener el token de la caché en memoria (operación rápida)
            var tokenDto = await _cache.GetAsync(sessionId);
            if (tokenDto != null && !isTokenExpired(tokenDto.AccessToken))
            {
                // Token válido y no expirado → devolverlo
                return tokenDto;
            }

            // 2. No está en caché o ha expirado → pedir renovación al módulo Auth
            //    Auth maneja internamente el refresh token (hash, validación, rotación, etc.)
            var renewalResult = await _refreshTokenService.RenewAccessTokenAsync(sessionId);
            if (renewalResult == null)
            {
                await _cache.RemoveAsync(sessionId);
                return null;
            }

            // 3. Obtuvimos un nuevo access token (y posiblemente un nuevo refresh token,
            //    pero el BFF no lo necesita porque no lo usa directamente)

            var newTokenDto = new TokenDto()
            {
                AccessToken = renewalResult.AccessToken
            };

            // 4. Guardar el nuevo access token en caché con su tiempo de expiración

            await _cache.SetAsync(sessionId, newTokenDto);

            return newTokenDto;
        }




        private bool isTokenExpired(string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken)) return true;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(AccessToken);
                return jwt.ValidTo < DateTime.UtcNow;
            }
            catch (System.Exception)
            {

                return true;
            }
        }
    }
}
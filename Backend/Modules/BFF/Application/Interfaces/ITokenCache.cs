
using Backend.Modules.BFF.Application.DTOs;

namespace Backend.Modules.BFF.Application.Interfaces
{
    public interface ITokenCache
    {
        Task<TokenDto?> GetAsync(string sessionId);

        Task SetAsync(string sessionId, TokenDto dto);

        Task RemoveAsync(string sessionId);
    }
}
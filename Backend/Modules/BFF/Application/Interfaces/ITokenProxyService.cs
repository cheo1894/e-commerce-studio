using Backend.Modules.BFF.Application.DTOs;

namespace Backend.Modules.BFF.Application.Interfaces
{
    public interface ITokenProxyService
    {
        Task<TokenDto?> GetValidTokenAsync(string sessionId);
    }
}
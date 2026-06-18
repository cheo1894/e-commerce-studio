
using Backend.Modules.BFF.Application.DTOs;
using Backend.Modules.BFF.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Modules.BFF.Infrastructure.Cache
{



    public class MemoryTokenCache : ITokenCache
    {
        private readonly IMemoryCache _cache;

        public MemoryTokenCache(IMemoryCache cache)
        {
            _cache = cache;
        }
        public Task<TokenDto?> GetAsync(string sessionId)
        {
            _cache.TryGetValue(sessionId, out TokenDto dto);
            return Task.FromResult(dto);
        }
        public Task SetAsync(string sessionId, TokenDto dto)
        {
            _cache.Set(sessionId, dto, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string sessionId)
        {
            _cache.Remove(sessionId);
            return Task.CompletedTask;
        }


    }



}
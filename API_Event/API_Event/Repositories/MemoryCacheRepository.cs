using API_Event.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace API_Event.Repositories
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<string?> GetCache(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return null;

                var data = _memoryCache.TryGetValue(key, out string value);

                if (data == true)
                    return value;

                return default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string?> SetCache(string key, string value, TimeSpan expireIn)
        {
            try
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions 
                { 
                    AbsoluteExpiration = DateTime.Now.AddHours(expireIn.TotalHours),
                    Priority = CacheItemPriority.High,
                    Size = 1024,
                };

                _memoryCache.Set(key, value, options);
                return await GetCache(key);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task DeleteCache(string key)
        {
            throw new NotImplementedException();
        }

    }
}
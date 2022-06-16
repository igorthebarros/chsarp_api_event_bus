using System.Text.Json.Nodes;

namespace API_Event.Repositories.Interfaces
{
    public interface IMemoryCacheRepository
    {
        Task<string?> GetCache(string key);
        Task<string?> SetCache(string key, string value, TimeSpan expireIn);
        Task DeleteCache(string key);
    }
}
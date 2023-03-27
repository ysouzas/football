using System.Text.Json;
using F.Core.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace F.API.Data.Cache;

public class Cache : ICache
{
    private readonly IDistributedCache _cache;

    public Cache(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetCacheDataAsync<T>(string cacheKey)
    {
        // Get cache data using cache key
        string cacheData = await _cache.GetStringAsync(cacheKey);

        // Check if the cache data response contains data
        if (!string.IsNullOrEmpty(cacheData))
        {
            // It did, let's deserialize it and return it
            return JsonSerializer.Deserialize<T>(cacheData);
        }

        // We did not get any data return T
        return default;
    }

    public async Task RemoveCacheDataAsync(string cacheKey)
    {
        // Remove the cache data
        await _cache.RemoveAsync(cacheKey);
    }

    public async Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow = 10.0, double slidingExpiration = 5.0)
    {
        /// Configure cache expiration
        DistributedCacheEntryOptions cacheExpiry = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absExpRelToNow),
            SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
        };

        // Set the cache
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cacheValue), cacheExpiry);
    }
}
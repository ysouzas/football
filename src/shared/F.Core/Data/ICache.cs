namespace F.Core.Data;

public interface ICache
{
    Task<T> GetCacheDataAsync<T>(string cacheKey);

    Task RemoveCacheDataAsync(string cacheKey);

    Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow = 10.0, double slidingExpiration = 5.0);
}

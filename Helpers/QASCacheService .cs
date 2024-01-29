using Microsoft.Extensions.Caching.Memory;

namespace QuestionsAskingServer.Helpers
{
    public class QASCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public QASCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFromDb, TimeSpan? expiryTime = null)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out T? cachedData))
            {
                cachedData = await fetchFromDb();

                if (cachedData == null)
                {
                    throw new InvalidOperationException("The fetch operation returned null, which is not allowed.");
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiryTime ?? TimeSpan.FromMinutes(30)
                };
                _memoryCache.Set(cacheKey, cachedData, cacheEntryOptions);
            }

            return cachedData!;
        }
        public void Invalidate(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}

namespace QuestionsAskingServer.Helpers
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFromDb, TimeSpan? expiryTime = null);
        void Invalidate(string cacheKey);
    }
}

using StackExchange.Redis;
using System.Text.Json;

namespace Services.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponseJson = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey, serializedResponseJson , timeToLive);
        }
        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var CachedResponse = await _database.StringGetAsync(cacheKey);

            if (CachedResponse.IsNullOrEmpty)
                return null;

            return CachedResponse;

        }

    }
}

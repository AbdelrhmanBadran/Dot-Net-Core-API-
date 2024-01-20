using Infrastructure.BasketRepository.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
            => await _database.KeyDeleteAsync(BasketId);

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var data = await _database.StringGetAsync(BasketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket)
        {
            var IsCreated = await _database.StringSetAsync(Basket.Id, JsonSerializer.Serialize(Basket), TimeSpan.FromDays(30));

            if (!IsCreated)
                return null;

            return await GetBasketAsync(Basket.Id);
        }
    }
}

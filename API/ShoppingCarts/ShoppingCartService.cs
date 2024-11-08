using System;
using System.Text.Json;
using StackExchange.Redis;

namespace API.ShoppingCarts;

public class ShoppingCartService(IConnectionMultiplexer redis) : IShoppingCartService
{
    private readonly IDatabase redis = redis.GetDatabase();

    public Task<bool> DeleteShoppingCartAsync(string key)
    {
        return redis.KeyDeleteAsync(key);
    }

    public async Task<ShoppingCart?> GetShoppingCartAsync(string key)
    {
        var data = await redis.StringGetAsync(key);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data!);
    }

    public async Task<ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart)
    {
        var created = await redis.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetShoppingCartAsync(cart.Id);
    }
}

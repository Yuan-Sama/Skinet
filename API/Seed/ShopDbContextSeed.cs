using System.Text.Json;
using API.Core;
using API.ShopProducts;

namespace API.Seed;

public class ShopDbContextSeed
{
    public static async Task SeedAsync(ShopDbContext shopDbContext)
    {
        if (!shopDbContext.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("./Seed/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products is null) return;

            shopDbContext.Products.AddRange(products);

            await shopDbContext.SaveChangesAsync();
        }
    }
}

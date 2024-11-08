using System;

namespace API.ShoppingCarts;

public interface IShoppingCartService
{
    Task<ShoppingCart?> GetShoppingCartAsync(string key);
    Task<ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart);
    Task<bool> DeleteShoppingCartAsync(string key);
}

using Microsoft.AspNetCore.Mvc;

namespace API.ShoppingCarts;

[Route("api/[controller]")]
[ApiController]
public class CartController(IShoppingCartService shoppingCartService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetShoppingCartByIdAsync(string id)
    {
        var cart = await shoppingCartService.GetShoppingCartAsync(id);

        return cart ?? new ShoppingCart { Id = id };
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart?>> UpdateShoppingCartAsync(ShoppingCart cart)
    {
        var updatedCart = await shoppingCartService.SetShoppingCartAsync(cart);

        if (updatedCart == null) return BadRequest("Problem with cart");

        return updatedCart;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteShoppingCartAsync(string id)
    {
        var result = await shoppingCartService.DeleteShoppingCartAsync(id);

        if (!result) return BadRequest("Problem deleting cart");

        return Ok();
    }
}

using API.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.ShopProducts;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ShopDbContext shopDbContext;

    public ProductsController(ShopDbContext shopDbContext)
    {
        this.shopDbContext = shopDbContext;
    }

    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await shopDbContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await shopDbContext.Products.FindAsync(id);

        if (product is null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        shopDbContext.Products.Add(product);

        await shopDbContext.SaveChangesAsync();

        return product;
    }
}

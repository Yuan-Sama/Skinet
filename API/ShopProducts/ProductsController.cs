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

    public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
    {
        return await shopDbContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        var product = await shopDbContext.Products.FindAsync(id);

        if (product is null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(Product product)
    {
        shopDbContext.Products.Add(product);

        await shopDbContext.SaveChangesAsync();

        return product;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProductAsync(int id, Product newProduct)
    {
        if (newProduct.Id != id || !shopDbContext.Products.Any(p => p.Id == id))
            return BadRequest("Cannot update product");

        shopDbContext.Entry(newProduct).State = EntityState.Modified;

        await shopDbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        var product = await shopDbContext.Products.FindAsync(id);

        if (product is null) return NotFound();

        shopDbContext.Products.Remove(product);

        await shopDbContext.SaveChangesAsync();

        return NoContent();
    }
}

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

    public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync([FromQuery] ProductsQueries productsQueries)
    {
        var query = shopDbContext.Products.AsQueryable();

        query = query.Where(p => productsQueries.Brands.Count == 0 || productsQueries.Brands.Contains(p.Brand));

        query = query.Where(p => productsQueries.Types.Count == 0 || productsQueries.Types.Contains(p.Type));

        query = productsQueries.Sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        return await query.ToListAsync();
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

        var numberOfProduct = await shopDbContext.SaveChangesAsync();

        if (numberOfProduct < 1)
            return BadRequest("Problem creating product");

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProductAsync(int id, Product newProduct)
    {
        if (newProduct.Id != id || !shopDbContext.Products.Any(p => p.Id == id))
            return BadRequest("Cannot update product");

        shopDbContext.Entry(newProduct).State = EntityState.Modified;

        var numberOfProduct = await shopDbContext.SaveChangesAsync();

        if (numberOfProduct < 1)
            return BadRequest("Problem updating the product");

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        var product = await shopDbContext.Products.FindAsync(id);

        if (product is null) return NotFound();

        shopDbContext.Products.Remove(product);

        var numberOfProduct = await shopDbContext.SaveChangesAsync();

        if (numberOfProduct < 1)
            return BadRequest("Problem deleting the product");

        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrandsAsync()
    {
        return await shopDbContext.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypesAsync()
    {
        return await shopDbContext.Products.Select(p => p.Type).Distinct().ToListAsync();
    }
}

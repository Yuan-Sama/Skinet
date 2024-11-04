using API.ShopProducts;
using Microsoft.EntityFrameworkCore;

namespace API.Core;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}

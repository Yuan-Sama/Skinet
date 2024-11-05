using API.Core;
using API.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ShopDbContext>();

    await context.Database.MigrateAsync();
    await ShopDbContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();

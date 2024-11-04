using API.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Development"));
});

var app = builder.Build();

app.MapControllers();

app.Run();

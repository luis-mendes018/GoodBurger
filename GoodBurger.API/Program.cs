using GoodBurger.API.Database;
using GoodBurger.API.Features.Menu;
using GoodBurger.API.Features.Orders.Create;
using GoodBurger.API.Features.Orders.Delete;
using GoodBurger.API.Features.Orders.Read;
using GoodBurger.API.Features.Orders.Update;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Good Hamburger API")
               .WithTheme(ScalarTheme.Moon)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

//Menu
app.MapGetMenuEndpoint();

//Orders
app.MapCreateOrderEndpoint();
app.MapListOrdersEndpoint();
app.MapGetOrderByIdEndpoint();
app.MapGetOrdersByClientNameEndpoint();
app.MapUpdateOrderEndpoint();
app.MapDeleteOrderEndpoint();

app.UseHttpsRedirection();

app.Run();




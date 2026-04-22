using GoodBurger.API.Entities.Products;

namespace GoodBurger.API.Features.Menu;

public static class GetMenu
{
    public record MenuResponse(List<ProductDto> Sandwiches, List<ProductDto> Sides);

    public static void MapGetMenuEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("v1/menu", async () =>
        {
            var menu = await Task.Run(() => new MenuResponse(
                Sandwiches: [.. MenuStore.Products
                    .Where(p => p.Type == ProductType.Sandwich)
                    .Select(p => new ProductDto(p.Id, p.Name, p.Price))],

                Sides: [.. MenuStore.Products
                    .Where(p => p.Type == ProductType.Side)
                    .Select(p => new ProductDto(p.Id, p.Name, p.Price))]
            ));

            return Results.Ok(menu);
        })
     .WithName("GetMenu")
     .WithTags("Menu");
    }
}

using GoodBurger.API.Database;

namespace GoodBurger.API.Features.Orders.Delete;

public static class DeleteOrder
{
    public static void MapDeleteOrderEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("v1/orders/delete/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            
            var order = await db.Orders.FindAsync(id);

            if (order is null)
            {
                return Results.NotFound(new
                {
                    error = "Não encontrado",
                    message = $"O pedido com ID {id} não existe."
                });
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteOrder")
        .WithTags("Orders");
    }
}

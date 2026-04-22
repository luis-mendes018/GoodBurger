using GoodBurger.API.Database;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.API.Features.Orders.Read;

public static class GetOrderById
{
   
    public static void MapGetOrderByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("v1/orders/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var order = await db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return Results.NotFound("Pedido não encontrado.");

            var response = new OrderDetailResponse(
                order.Id,
                order.NameClient,
                order.Subtotal,
                order.Discount,
                order.Total,
                order.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                [.. order.OrderItems.Select(i => new OrderItemDto(i.ProductName, i.UnitPrice))]
            );

            return Results.Ok(response);
        })
        .WithName("GetOrderById")
        .WithTags("Orders");
    }
}

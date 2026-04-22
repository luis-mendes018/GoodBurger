using GoodBurger.API.Database;
using GoodBurger.API.Features.Orders.Create;
using GoodBurger.API.Shared;

using Microsoft.EntityFrameworkCore;

namespace GoodBurger.API.Features.Orders.Read;

public static class GetOrdersByClientName
{
    public static void MapGetOrdersByClientNameEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("v1/orders/search", async (
            AppDbContext db,
            string name,
            int page = 1,
            int pageSize = 10) =>
        {
            
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            var query = db.Orders.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(o =>
                    EF.Functions.ILike(o.NameClient, $"%{name}%"));
            }

            var totalItems = await query.CountAsync();

            var data = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderResponse(
                    o.Id,
                    o.NameClient,
                    o.Subtotal,
                    o.Discount,
                    o.Total,
                    o.CreatedAt.ToString("dd/MM/yyyy HH:mm")
                ))
                .ToListAsync();

            var response = new PagedResponse<OrderResponse>(
                data,
                totalItems,
                page,
                pageSize
            );

            return Results.Ok(response);
        })
        .WithName("GetOrdersClientName")
        .WithTags("Orders");
    }
}
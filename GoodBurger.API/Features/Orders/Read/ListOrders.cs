using GoodBurger.API.Database;
using GoodBurger.API.Shared;

using Microsoft.EntityFrameworkCore;

namespace GoodBurger.API.Features.Orders.Read;


public static class ListOrders
{
    public static void MapListOrdersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("v1/orders", async (AppDbContext db, int page = 1, int pageSize = 10) =>
        {
            
            if (pageSize < 1) pageSize = 10;

            
            var totalItems = await db.Orders.CountAsync();

            
            var orders = await db.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)              
                .Select(o => new OrderListItemResponse(
                    o.Id,
                    o.NameClient,
                    o.Total,
                    o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    o.OrderItems.Select(i => new OrderItemDto(i.ProductName, i.UnitPrice)).ToList()
                ))
                .ToListAsync();

           
            var response = new PagedResponse<OrderListItemResponse>(orders, totalItems, page, pageSize);

            return Results.Ok(response);
        })
        .WithName("ListOrders")
        .WithTags("Orders");
    }
}

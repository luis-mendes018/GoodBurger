using GoodBurger.API.Database;
using GoodBurger.API.Entities.Orders;
using GoodBurger.API.Entities.Products;
using GoodBurger.API.Features.Orders.Create;
using GoodBurger.API.Shared;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.API.Features.Orders.Update;

public static class UpdateOrder
{
    public static void MapUpdateOrderEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("v1/orders/update/{id:guid}", async (Guid id, UpdateOrderRequest request, AppDbContext db) =>
        {
            var order = await db.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound(new { error = "Pedido não encontrado" });

            
            var foundProducts = MenuStore.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .ToList();

            
            var (isValid, errorMessage) = OrderDomainService.ValidateOrderRules(request.ProductIds);
            if (!isValid)
                return Results.BadRequest(new { error = "Erro de Validação", message = errorMessage });

            
            var totals = OrderDomainService.CalculateTotals(foundProducts);

            order.Subtotal = totals.Subtotal;
            order.Discount = totals.Discount;
            order.Total = totals.Total;

            
            var existingItems = await db.OrderItems
                .Where(i => i.OrderId == order.Id)
                .ToListAsync();

            db.OrderItems.RemoveRange(existingItems);

            
            foreach (var product in foundProducts)
            {
                db.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    OrderId = order.Id
                });
            }

            await db.SaveChangesAsync();

            return Results.Ok(new OrderResponse(
                order.Id,
                order.NameClient,
                order.Subtotal,
                order.Discount,
                order.Total,
                order.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")));
        })
        .WithName("UpdateOrder")
        .WithTags("Orders");
    }
}
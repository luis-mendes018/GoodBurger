using GoodBurger.API.Database;
using GoodBurger.API.Entities.Orders;
using GoodBurger.API.Entities.Products;
using GoodBurger.API.Shared;

namespace GoodBurger.API.Features.Orders.Create;

public static class CreateOrder
{
    public static void MapCreateOrderEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("v1/orders/post", async (CreateOrderRequest request, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(request.NameClient))
            {
                return Results.BadRequest(new
                {
                    error = "Erro de Validação",
                    message = "O nome do cliente é obrigatório."
                });
            }

            var foundProducts = MenuStore.Products
                .Where(p => request.ProductIds.Contains(p.Id))
                .ToList();

            var (isValid, errorMessage) = OrderDomainService
                  .ValidateOrderRules(request.ProductIds);

            if (!isValid)
                return Results.BadRequest(new { error = "Erro de Validação", message = errorMessage });

            var totals = OrderDomainService.CalculateTotals(foundProducts);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                NameClient = request.NameClient,
                Subtotal = totals.Subtotal,
                Discount = totals.Discount,
                Total = totals.Total,
                CreatedAt = DateTime.Now,
                OrderItems = [.. foundProducts.Select(p => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = p.Id,
                    ProductName = p.Name,
                    UnitPrice = p.Price
                })]
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return Results.Created($"/orders/{order.Id}", new OrderResponse(
                order.Id, order.NameClient, order.Subtotal, order.Discount, order.Total, order.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")));
        })
        .WithName("CreateOrder")
        .WithTags("Orders");
    }
}

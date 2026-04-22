namespace GoodBurger.API.Features.Orders.Create;

public record CreateOrderRequest(string NameClient, List<Guid> ProductIds);

public record OrderResponse(
    Guid Id,
    string NameClient,
    decimal Subtotal,
    decimal Discount,
    decimal Total,
    string CreatedAtFormatted);

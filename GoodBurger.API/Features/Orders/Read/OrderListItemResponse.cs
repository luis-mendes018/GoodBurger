namespace GoodBurger.API.Features.Orders.Read;

public record OrderListItemResponse(
    Guid Id,
    string NameClient,
    decimal Total,
    string CreatedAtFormatted,
    List<OrderItemDto> Items);

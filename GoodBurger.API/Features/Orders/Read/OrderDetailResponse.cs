namespace GoodBurger.API.Features.Orders.Read;

public record OrderDetailResponse(
       Guid Id,
       string NameClient,
       decimal Subtotal,
       decimal Discount,
       decimal Total,
       string CreatedAtFormatted,
       List<OrderItemDto> Items);

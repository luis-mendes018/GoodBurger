namespace GoodBurger.API.Features.Orders.Update;

public record UpdateOrderRequest(
    List<Guid> ProductIds
);

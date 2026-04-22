namespace GoodBurger.API.Entities.Orders;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NameClient { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

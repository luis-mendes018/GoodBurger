namespace GoodBurger.Client.ViewModels.Orders;

public class OrderResponseViewModel
{
    public Guid Id { get; set; }
    public string NameClient { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}

namespace GoodBurger.Client.ViewModels.Orders;

public class OrderDetailViewModel
{
    public string NameClient { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }

    public string CreatedAtFormatted { get; set; } = string.Empty;

    public List<OrderItemViewModel> Items { get; set; } = [];
}

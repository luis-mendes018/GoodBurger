using GoodBurger.Client.ViewModels.Products;

namespace GoodBurger.Client.ViewModels.Orders;

public class UpdateOrderViewModel
{
    public Guid Id { get; set; }

    public List<ProductOption> Products { get; set; } = [];

    public List<Guid> SelectedProductIds { get; set; } = [];
}

using GoodBurger.Client.ViewModels.Products;

using System.ComponentModel.DataAnnotations;

namespace GoodBurger.Client.ViewModels.Orders;

public class CreateOrderViewModel
{
    public string NameClient { get; set; } = string.Empty;

    public List<ProductOption> Products { get; set; } = [];

    public List<Guid> SelectedProductIds { get; set; } = [];
}
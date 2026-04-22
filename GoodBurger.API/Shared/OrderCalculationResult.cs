using GoodBurger.API.Entities.Products;

namespace GoodBurger.API.Shared;

public record OrderCalculationResult(decimal Subtotal, decimal Discount, decimal Total);

public static class OrderDomainService
{
    public static (bool IsValid, string ErrorMessage) ValidateOrderRules(
    List<Guid> requestedIds)
    {
        if (requestedIds == null || requestedIds.Count == 0)
        {
            return (false, "O pedido deve conter pelo menos um produto.");
        }

       
        var productsFromRequest = requestedIds
            .Select(id => MenuStore.Products.FirstOrDefault(p => p.Id == id))
            .ToList();

     
        if (productsFromRequest.Any(p => p == null))
        {
            return (false, "O pedido contém produtos inválidos.");
        }


        var sandwiches = productsFromRequest
            .Where(p => p!.Type == ProductType.Sandwich)
            .ToList();

        var fries = productsFromRequest
            .Where(p => p!.Name.Contains("Batata", StringComparison.OrdinalIgnoreCase))
            .ToList();

        var sodas = productsFromRequest
            .Where(p => p!.Name.Contains("Refrigerante", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (sandwiches.Count > 1)
            return (false, "Só pode ter um sanduíche no pedido.");

        if (fries.Count > 1)
            return (false, "Só pode ter uma batata frita no pedido.");

        if (sodas.Count > 1)
            return (false, "Só pode ter um refrigerante no pedido.");

        return (true, null);
    }

    public static OrderCalculationResult CalculateTotals(List<Product> products)
    {
        var hasSandwich = products.Any(p => p.Type == ProductType.Sandwich);
        var hasFries = products.Any(p => p.Id == MenuStore.FriesId);
        var hasSoda = products.Any(p => p.Id == MenuStore.SodaId);

        var subtotal = products.Sum(p => p.Price);
        decimal discountPercentage = 0;

        if (hasSandwich && hasFries && hasSoda)
            discountPercentage = 0.20m;
        else if (hasSandwich && hasSoda)
            discountPercentage = 0.15m;
        else if (hasSandwich && hasFries)
            discountPercentage = 0.10m;

        var discountValue = Math.Round(subtotal * discountPercentage, 2, MidpointRounding.AwayFromZero);

        var total = Math.Round(subtotal - discountValue, 2, MidpointRounding.AwayFromZero);

        return new OrderCalculationResult(subtotal, discountValue, total);
    }
}

namespace GoodBurger.API.Entities.Products;

public static class MenuStore
{
    public static readonly Guid XBurgerId = Guid.Parse("A1B2C3D4-E5F6-47A8-90B1-C2D3E4F5A6B7");
    public static readonly Guid XEggId = Guid.Parse("B2C3D4E5-F6A7-48B9-01C2-D3E4F5A6B7C8");
    public static readonly Guid XBaconId = Guid.Parse("C3D4E5F6-A7B8-49C9-12D3-E4F5A6B7C8D9");
    public static readonly Guid FriesId = Guid.Parse("D4E5F6A7-B8C9-4AD0-23D4-E5F6A7B8C9E0");
    public static readonly Guid SodaId = Guid.Parse("E5F6A7B8-C9D0-4BE1-34D5-F6A7B8C9D0E1");

    public static readonly List<Product> Products =
    [
        new Product(XBurgerId, "X Burger", 5.00m, ProductType.Sandwich),
        new Product(XEggId, "X Egg", 4.50m, ProductType.Sandwich),
        new Product(XBaconId, "X Bacon", 7.00m, ProductType.Sandwich),
        new Product(FriesId, "Batata frita", 2.00m, ProductType.Side),
        new Product(SodaId, "Refrigerante", 2.50m, ProductType.Side)
    ];
}

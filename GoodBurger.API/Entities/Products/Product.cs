namespace GoodBurger.API.Entities.Products;

public record Product(Guid Id, string Name, decimal Price, ProductType Type);

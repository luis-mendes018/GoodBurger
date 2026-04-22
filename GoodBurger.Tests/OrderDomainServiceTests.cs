using GoodBurger.API.Entities.Products;
using GoodBurger.API.Shared;

namespace GoodBurger.Tests;

public class OrderDomainServiceTests
{

    [Fact]
    public void Deve_Falhar_Quando_Lista_Vazia()
    {
        var ids = new List<Guid>();

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.False(isValid);
        Assert.Equal("O pedido deve conter pelo menos um produto.", error);
    }

    [Fact]
    public void Deve_Falhar_Quando_Produto_Invalido()
    {
        var ids = new List<Guid> { Guid.NewGuid() };

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.False(isValid);
        Assert.Equal("O pedido contém produtos inválidos.", error);
    }


    [Fact]
    public void Deve_Falhar_Com_Dois_Sanduiches()
    {
        var ids = new List<Guid>
        {
            MenuStore.XBurgerId,
            MenuStore.XEggId
        };

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.False(isValid);
        Assert.Equal("Só pode ter um sanduíche no pedido.", error);
    }


    [Fact]
    public void Deve_Falhar_Com_Duas_Batatas()
    {
        var ids = new List<Guid>
        {
            MenuStore.FriesId,
            MenuStore.FriesId
        };

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.False(isValid);
        Assert.Equal("Só pode ter uma batata frita no pedido.", error);
    }


    [Fact]
    public void Deve_Falhar_Com_Dois_Refrigerantes()
    {
        var ids = new List<Guid>
        {
            MenuStore.SodaId,
            MenuStore.SodaId
        };

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.False(isValid);
        Assert.Equal("Só pode ter um refrigerante no pedido.", error);
    }


    [Fact]
    public void Deve_Passar_Quando_Pedido_Valido()
    {
        var ids = new List<Guid>
        {
            MenuStore.XBurgerId,
            MenuStore.FriesId
        };

        var (isValid, error) = OrderDomainService.ValidateOrderRules(ids);

        Assert.True(isValid);
        Assert.Null(error);
    }



    [Fact]
    public void Deve_Calcular_Sem_Desconto()
    {
        var products = GetProducts(MenuStore.XBurgerId);

        var result = OrderDomainService.CalculateTotals(products);

        Assert.Equal(5.00m, result.Subtotal);
        Assert.Equal(0m, result.Discount);
        Assert.Equal(5.00m, result.Total);
    }

    [Fact]
    public void Deve_Aplicar_Desconto_10_Porcento()
    {
        var products = GetProducts(MenuStore.XBurgerId, MenuStore.FriesId);

        var result = OrderDomainService.CalculateTotals(products);

        Assert.Equal(7.00m, result.Subtotal);
        Assert.Equal(0.70m, result.Discount);
        Assert.Equal(6.30m, result.Total);
    }

    [Fact]
    public void Deve_Aplicar_Desconto_15_Porcento()
    {
        var products = GetProducts(MenuStore.XBurgerId, MenuStore.SodaId);

        var result = OrderDomainService.CalculateTotals(products);

        Assert.Equal(7.50m, result.Subtotal);
        Assert.Equal(1.13m, result.Discount);
        Assert.Equal(6.37m, result.Total);
    }

    [Fact]
    public void Deve_Aplicar_Desconto_20_Porcento()
    {
        var products = GetProducts(
            MenuStore.XBurgerId,
            MenuStore.FriesId,
            MenuStore.SodaId
        );

        var result = OrderDomainService.CalculateTotals(products);

        Assert.Equal(9.50m, result.Subtotal);
        Assert.Equal(1.90m, result.Discount);
        Assert.Equal(7.60m, result.Total);
    }



    private static List<Product> GetProducts(params Guid[] ids)
    {
        return ids
            .Select(id => MenuStore.Products.First(p => p.Id == id))
            .ToList();
    }
}
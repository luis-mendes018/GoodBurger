namespace GoodBurger.API.Shared;

public record PagedResponse<T>(List<T> Data, int TotalItems, int Page, int PageSize);

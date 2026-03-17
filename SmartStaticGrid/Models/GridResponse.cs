namespace SmartStaticGrid.Lib.Models;

public record GridResponse<T>(IEnumerable<T> Items, int TotalCount);


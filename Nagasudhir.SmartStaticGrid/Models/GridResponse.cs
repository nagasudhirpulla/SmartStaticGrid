namespace Nagasudhir.SmartStaticGrid.Models;

public record GridResponse<T>(IEnumerable<T> Items, int TotalCount);


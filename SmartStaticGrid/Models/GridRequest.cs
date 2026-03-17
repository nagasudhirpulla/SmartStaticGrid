namespace SmartStaticGrid.Lib.Models;

public record GridRequest(int Page, int Size, Dictionary<string, string> Filters, string? Sort, bool Desc, bool RequestNewCount);


namespace Nagasudhir.SmartStaticGrid.Models;
public class GridState
{
    public int TotalCount { get; set; } = 0;
    public int TargetPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortColumn { get; set; }
    public bool SortDescending { get; set; }
    public Dictionary<string, string> Filters { get; set; } = [];
}


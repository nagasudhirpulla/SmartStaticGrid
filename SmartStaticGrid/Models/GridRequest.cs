namespace SmartStaticGrid.Lib.Models;

/*
 * Page - Page number to fetch
 * PageSize - Number of items per page
 * Filters - Dictionary of column keys and their corresponding filter values
 * SortBy - The column key to sort by (optional)
 * SortDesc - Whether to sort in descending order
 * RequestNewCount - Whether to request a new total count of items (useful when filters change)
 */
public record GridRequest(int Page, int PageSize, Dictionary<string, string> Filters, string? SortBy, bool SortDesc, bool RequestNewCount);


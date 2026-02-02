namespace Blog.API.Core.Paginator;

public class Paginator<T>(int pageNumber, int pageSize, int totalItems, List<T> items)
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalItemsCount { get; set; } = totalItems;
    public List<T> Items { get; set; }  = items;
}
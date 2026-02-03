namespace Blog.API.Core.Paginator;

public class Paginator<T>
{
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    public required int TotalItemsCount { get; set; }
    public required List<T> Items { get; set; }
}
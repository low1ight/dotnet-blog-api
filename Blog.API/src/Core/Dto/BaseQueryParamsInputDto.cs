namespace Blog.API.Core.Dto;

public class BaseQueryParamsInputDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public SortOrder SortDirection { get; set; } = SortOrder.Desc;

    public bool IsAsc()
    {
        return SortDirection == SortOrder.Asc;
    }
    
}
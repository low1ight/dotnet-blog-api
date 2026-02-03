using Blog.API.Core.Dto;

namespace Blog.API.Modules.Post.Controllers.InputDto;

public record PostQueryParams(
    string SortBy = "Id",
    string? TitleSearchTerm = null,
    
    int PageNumber = 1,
    int PageSize = 20,
    SortOrder SortDirection = SortOrder.Desc
) : BaseQueryParams(PageNumber, PageSize, SortDirection);
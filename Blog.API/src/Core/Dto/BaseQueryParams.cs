using System.ComponentModel;

namespace Blog.API.Core.Dto;

public record BaseQueryParams(
    int PageNumber = 1,
    int PageSize = 20,
    SortOrder SortDirection = SortOrder.Desc
);
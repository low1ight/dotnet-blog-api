using Blog.API.Core.Dto;

namespace Blog.API.Modules.Post.Controllers.InputDto;

public class PostQueryParams : BaseQueryParamsInputDto
{
    public PostFields SortBy { get; set; } = PostFields.Id;
    public string? TitleSearchTerm { get; set; } = null;
}


public enum PostFields {
    Id,
    Title,
    Content, 
    Description,
}
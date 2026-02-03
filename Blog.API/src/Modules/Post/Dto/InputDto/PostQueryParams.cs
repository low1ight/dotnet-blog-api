namespace Blog.API.Modules.Post.Controllers.InputDto;

public record PostQueryParams(
    string SortBy = "Id",
    string? TitleSearchTerm = null
);
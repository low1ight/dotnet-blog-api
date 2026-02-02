namespace Blog.API.Modules.Post.Controllers.InputDto;

public class CreatePostDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
}
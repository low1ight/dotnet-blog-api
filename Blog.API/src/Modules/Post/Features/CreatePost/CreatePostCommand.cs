using MediatR;

namespace Blog.API.Modules.Post.Features.CreatePost;

public record CreatePostCommand(
    string Title,
    string Description,
    string Content
) : IRequest<int>;

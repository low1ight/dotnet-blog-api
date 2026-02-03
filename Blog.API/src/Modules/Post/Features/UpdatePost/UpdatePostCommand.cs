using MediatR;

namespace Blog.API.Modules.Post.Features.UpdatePost;

public record UpdatePostCommand(
        int id,
        string Title,
        string Description,
        string Content
    ) : IRequest<bool>;

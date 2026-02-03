using Blog.API.Data;
using MediatR;

namespace Blog.API.Modules.Post.Features.CreatePost;

public class CreatePostCommandHandler(AppDbContext context) : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Domain.Post
        {
            Title = request.Title,
            Content = request.Content,
            Description = request.Description,
        };
        
        context.Posts.Add(post);
        await context.SaveChangesAsync(cancellationToken);
        
        return post.Id;
    }
}
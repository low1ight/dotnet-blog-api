using Blog.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Features.UpdatePost;

public class UpdatePostCommandHandler(AppDbContext context) : IRequestHandler<UpdatePostCommand,bool>
{
    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await context.Posts.Where(c => c.Id == request.id).FirstOrDefaultAsync(cancellationToken);
            
        if (post is null) return false;
        
        post.Title = request.Title;
        post.Content = request.Content;
        post.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
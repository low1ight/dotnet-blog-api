using Blog.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Features.DeletePostById;

public class DeletePlayerByIdCommandHandler(AppDbContext context) : IRequestHandler<DeletePostByIdCommand, bool>
{
    public async Task<bool> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
    {
        var post = await context.Posts.Where(c => c.Id == request.id).FirstOrDefaultAsync(cancellationToken);
            
        if (post is null) return false;
        
        context.Posts.Remove(post);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
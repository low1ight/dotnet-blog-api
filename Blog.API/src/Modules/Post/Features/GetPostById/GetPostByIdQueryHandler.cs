using Blog.API.Data;
using Blog.API.Modules.Post.Controllers.ViewDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Features.GetPostById;

public class GetPostByIdQueryHandler(AppDbContext context) : IRequestHandler<GetPostByIdQuery, PostViewDto?>
{
    public async Task<PostViewDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Where(post => post.Id == request.id)
            .Select(post => new PostViewDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            }).FirstOrDefaultAsync(cancellationToken);
        
    }
}
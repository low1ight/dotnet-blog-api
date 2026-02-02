using Blog.API.Data;
using Blog.API.Modules.Post.Controllers.ViewDto;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Infrastructure.QueryRepository;

public class PostsQueryRepository(AppDbContext context) : IPostsQueryRepository
{
    public async Task<List<PostViewDto>> GetAllPosts()
        => await context.Posts.Select(post => new PostViewDto
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
        }).ToListAsync();


    public async Task<PostViewDto?> GetPostById(int id)
        => await context.Posts
            .Where(post => post.Id == id)
            .Select(post => new PostViewDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            }).FirstOrDefaultAsync();
}
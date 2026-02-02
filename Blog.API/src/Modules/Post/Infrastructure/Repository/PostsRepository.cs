using Blog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Infrastructure.Repository;

public class PostsRepository(AppDbContext context) : IPostsRepository
{
    public async Task<bool> CreatePost(Domain.Post post)
    {
        context.Posts.Add(post);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePost(Domain.Post post)
    {
        context.Posts.Update(post);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePost(Domain.Post post)
    {
        context.Posts.Remove(post);
        return await context.SaveChangesAsync() > 0;
    }

    public Task<Domain.Post?> GetPostById(int id)
        => context.Posts.Where(c => c.Id == id).FirstOrDefaultAsync();
}
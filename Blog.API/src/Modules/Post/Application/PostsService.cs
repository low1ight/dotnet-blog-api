using Blog.API.Data;
using Blog.API.Modules.Post.Controllers.InputDto;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Application;

public class PostsService(AppDbContext context)
{
    public async Task<int> CreatePostAsync(PostInputDto dto)
    {
        var post = new Domain.Post
        {
            Title = dto.Title,
            Content = dto.Content,
            Description = dto.Description,
        };
        
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        
        return post.Id;
        
    }

    public async Task<bool> UpdatePostAsync(PostInputDto dto,int id)
    {
        var post = await context.Posts.Where(c => c.Id == id).FirstOrDefaultAsync();
            
        if (post is null) return false;
        
        post.Title = dto.Title;
        post.Content = dto.Content;
        post.Description = dto.Description;

        await context.SaveChangesAsync();
        
        return true;
    }


    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await context.Posts.Where(c => c.Id == id).FirstOrDefaultAsync();
            
        if (post is null) return false;
        
        context.Posts.Remove(post);

        await context.SaveChangesAsync();

        return true;
    }
}
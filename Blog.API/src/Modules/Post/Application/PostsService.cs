using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Infrastructure.Repository;

namespace Blog.API.Modules.Post.Application;

public class PostsService(IPostsRepository repository) : IPostsService
{
    public async Task<int> CreatePostAsync(PostInputDto dto)
    {
        var post = new Domain.Post
        {
            Title = dto.Title,
            Content = dto.Content,
            Description = dto.Description,
        };
        
        await repository.CreatePost(post);
        
        return post.Id;
        
    }

    public async Task<bool> UpdatePostAsync(PostInputDto dto,int id)
    {
        var post = await repository.GetPostById(id);
            
        if (post is null) return false;
        
        post.Title = dto.Title;
        post.Content = dto.Content;
        post.Description = dto.Description;

        await repository.UpdatePost(post);
        
        return true;
    }


    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await repository.GetPostById(id);
            
        if (post is null) return false;
        
        await repository.DeletePost(post);

        return true;
    }
}
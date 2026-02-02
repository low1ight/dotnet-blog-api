using Blog.API.Modules.Post.Controllers.InputDto;

namespace Blog.API.Modules.Post.Application;

public interface IPostsService
{
    Task<int> CreatePostAsync(CreatePostDto dto);

    Task<bool> UpdatePostAsync(UpdatePostDto dto, int id);

    Task<bool> DeletePostAsync(int id);
}
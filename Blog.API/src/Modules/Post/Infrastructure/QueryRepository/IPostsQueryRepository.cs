using Blog.API.Modules.Post.Controllers.ViewDto;

namespace Blog.API.Modules.Post.Infrastructure.QueryRepository;


public interface IPostsQueryRepository
{
   Task<List<PostViewDto>> GetAllPosts();
   
   Task<PostViewDto?> GetPostById(int id);

}
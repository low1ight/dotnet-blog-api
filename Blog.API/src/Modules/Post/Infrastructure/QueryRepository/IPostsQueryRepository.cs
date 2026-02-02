using Blog.API.Core.Paginator;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;

namespace Blog.API.Modules.Post.Infrastructure.QueryRepository;


public interface IPostsQueryRepository
{
   Task<Paginator<PostViewDto>> GetAllPosts(PostQueryParams queryParams);
   
   Task<PostViewDto?> GetPostById(int id);

}
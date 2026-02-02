using Blog.API.Modules.Post.Controllers.ViewDto;
using Blog.API.Modules.Post.Infrastructure.QueryRepository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Modules.Post.Controllers;


[ApiController]
[Route("[controller]")]
public class PostsController(IPostsQueryRepository postsQueryRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PostViewDto>>> GetAllPosts()
        => await postsQueryRepository.GetAllPosts();

    [HttpGet("id")]
    public async Task<ActionResult<PostViewDto>> GetAllPosts(int id)
    {
        var post = await postsQueryRepository.GetPostById(id);
        return post is null ? NotFound() : Ok(post); 
    }
        
}
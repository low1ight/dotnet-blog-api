using Blog.API.Modules.Post.Application;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;
using Blog.API.Modules.Post.Infrastructure.QueryRepository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Modules.Post.Controllers;


[ApiController]
[Route("[controller]")]
public class PostsController(
    IPostsQueryRepository postsQueryRepository,
    IPostsService postsService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PostViewDto>>> GetAllPosts()
        => Ok(await postsQueryRepository.GetAllPosts());

    [HttpGet("id")]
    public async Task<ActionResult<PostViewDto>> GetAllPosts(int id)
    {
        var post = await postsQueryRepository.GetPostById(id);
        return post is null ? NotFound() : Ok(post); 
    }
    
    [HttpPost]
    public async Task<ActionResult<PostViewDto>> CreatePost(CreatePostDto dto)
    {
        var createdPostId = await postsService.CreatePostAsync(dto);
        var post = await postsQueryRepository.GetPostById(createdPostId);
        return CreatedAtAction(nameof(GetAllPosts), new { id = createdPostId }, post);
    }
    
    
    [HttpPut("id")]
    public async Task<ActionResult<PostViewDto>> UpdatePost(UpdatePostDto dto, int id)
    {
        bool result = await postsService.UpdatePostAsync(dto, id);
        return result ? NoContent() : NotFound();
    }
    
    [HttpDelete("id")]
    public async Task<ActionResult<PostViewDto>> DeletePost(int id)
    {
        bool result = await postsService.DeletePostAsync(id);
        return result ? NoContent() : NotFound();
    }
        
}
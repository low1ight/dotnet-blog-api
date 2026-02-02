using Blog.API.Core.Paginator;
using Blog.API.Modules.Post.Application;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;
using Blog.API.Modules.Post.Infrastructure.QueryRepository;
using FluentValidation;
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
    public async Task<ActionResult<Paginator<PostViewDto>>> GetAllPosts([FromQuery] PostQueryParams queryParams)
    {
        return Ok(await postsQueryRepository.GetAllPosts(queryParams));
    }
        

    [HttpGet("id")]
    public async Task<ActionResult<PostViewDto>> GetAllPosts(int id)
    {
        var post = await postsQueryRepository.GetPostById(id);
        return post is null ? NotFound() : Ok(post); 
    }
    
    [HttpPost]
    public async Task<ActionResult<PostViewDto>> CreatePost(PostInputDto dto, IValidator<PostInputDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary());
            return new BadRequestObjectResult(problemDetails);
        }
        
        
        var createdPostId = await postsService.CreatePostAsync(dto);
        var post = await postsQueryRepository.GetPostById(createdPostId);
        return CreatedAtAction(nameof(GetAllPosts), new { id = createdPostId }, post);
    }
    
    
    [HttpPut("id")]
    public async Task<ActionResult<PostViewDto>> UpdatePost(PostInputDto dto, int id, IValidator<PostInputDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary());
            return new BadRequestObjectResult(problemDetails);
        }
        
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
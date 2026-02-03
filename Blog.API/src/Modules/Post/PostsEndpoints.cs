using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Features.CreatePost;
using Blog.API.Modules.Post.Features.DeletePostById;
using Blog.API.Modules.Post.Features.GetAllPosts;
using Blog.API.Modules.Post.Features.GetPostById;
using Blog.API.Modules.Post.Features.UpdatePost;
using MediatR;

namespace Blog.API.Modules.Post;

public static class PostsEndpoints
{
    public static RouteGroupBuilder MapPostsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
                ISender sender,
                [AsParameters] PostQueryParams queryParams)
            => await sender.Send(new GetAllPostsQuery(queryParams)));


        group.MapGet("/{id:int}", async (ISender sender, int id) =>
        {
            var post = await sender.Send(new GetPostByIdQuery(id));
            return post is not null ? Results.Ok(post) : Results.NotFound();
        });


        group.MapPost("/", async (ISender sender, CreatePostCommand command) =>
        {
            var createdUserId = await sender.Send(command);
            return Results.Created($"api/posts/{createdUserId}", createdUserId);
        });


        group.MapPut("/{id:int}", async (ISender sender, PostInputDto dto, int id) =>
        {
            bool result = await sender.Send(new UpdatePostCommand(id, dto.Title, dto.Description, dto.Content));
            return result ? Results.NoContent() : Results.NotFound();
        });


        group.MapDelete("/{id:int}", async (ISender sender, int id) =>
        {
            bool result = await sender.Send(new DeletePostByIdCommand(id));
            return result ? Results.NoContent() : Results.NotFound();
        });


        return group;
    }
}
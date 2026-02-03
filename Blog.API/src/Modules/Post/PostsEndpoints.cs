using Blog.API.Core.Paginator;
using Blog.API.Data;
using Blog.API.Modules.Post.Application;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Blog.API.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Modules.Post;

public static class PostsEndpoints
{
    public static RouteGroupBuilder MapPostsEndpoints(this RouteGroupBuilder group)
    {

        group.MapGet("/", async (
            AppDbContext context, 
            [AsParameters] BaseQueryParams baseQuery,
            [AsParameters] PostQueryParams postQuery) =>
        {
            
            
            Console.WriteLine("dir: " + baseQuery);
            Console.WriteLine("dir: " + postQuery);
            
            
            var query = context.Posts.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(postQuery.TitleSearchTerm))
            {
                query = query.Where(p => p.Title.Contains(postQuery.TitleSearchTerm));
            }
            
            var sortDirection = baseQuery.SortDirection == SortOrder.Asc ? "asc" : "desc";
            
            var sortBy = string.IsNullOrWhiteSpace(postQuery.SortBy) 
                ? "Id" 
                : postQuery.SortBy;

            query = query.OrderBy($"{sortBy} {sortDirection}");
            
            
            var totalCount = await query.CountAsync();
            
            
            var result = await query
                .Skip((baseQuery.PageNumber - 1) * baseQuery.PageSize)
                .Take(baseQuery.PageSize)
                .Select(post => new PostViewDto
            
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Content = post.Content,
                }).ToListAsync();
            
            return new Paginator<PostViewDto>
            {
                PageNumber = baseQuery.PageNumber,
                PageSize = result.Count,
                TotalItemsCount = totalCount,
                Items = result
            };
            




        });
        
        
        
        group.MapGet("/{id:int}", async (AppDbContext context, int id) =>
        {
            var post = await context.Posts
                .Where(post => post.Id == id)
                .Select(post => new PostViewDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Content = post.Content,
                }).FirstOrDefaultAsync();
            
            return post is not null ? Results.Ok(post) : Results.NotFound();
        });
        
        
        
        
        
        group.MapPost("/", async (PostsService postsService, PostInputDto dto) =>
        {
             var createdUserId = await postsService.CreatePostAsync(dto);
             return Results.Created();
            
        });
        
        
        group.MapPut("/{id:int}", async (PostsService postsService, PostInputDto dto, int id) =>
        {
            bool result = await postsService.UpdatePostAsync(dto, id);
            return result ? Results.NoContent() : Results.NotFound();
            
        });
        
        
        group.MapDelete("/{id:int}", async (PostsService postsService, int id) =>
        {
            bool result = await postsService.DeletePostAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
            
        });
        
        

        return group;
    }
}
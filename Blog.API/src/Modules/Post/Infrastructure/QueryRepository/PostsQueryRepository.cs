using System.Linq.Expressions;
using Blog.API.Core.Paginator;
using Blog.API.Data;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Infrastructure.QueryRepository;

public class PostsQueryRepository(AppDbContext context) : IPostsQueryRepository
{
    private static readonly Dictionary<PostFields, Expression<Func<Domain.Post, object>>>
        AvailablePostFields = new()
        {
            [PostFields.Title] = p => p.Title,
            [PostFields.Content] = p => p.Content,
            [PostFields.Description] = p => p.Description
        };

    public async Task<Paginator<PostViewDto>> GetAllPosts(PostQueryParams queryParams)
    {
        var query = context.Posts.AsQueryable();

        if (queryParams.TitleSearchTerm is not null)
        {
            query = query.Where(p => p.Title.Contains(queryParams.TitleSearchTerm));
        }
        
        var sortBy = AvailablePostFields.GetValueOrDefault(queryParams.SortBy, p => p.Id);
        query = queryParams.IsAsc() ? query.OrderBy(sortBy) : query.OrderByDescending(sortBy);
        
        
        var totalCount = await query.CountAsync();
        
        


        var result = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Select(post => new PostViewDto

            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            }).ToListAsync();

        return new Paginator<PostViewDto>(queryParams.PageNumber, result.Count, totalCount, result);
    }


    public async Task<PostViewDto?> GetPostById(int id)
        => await context.Posts
            .Where(post => post.Id == id)
            .Select(post => new PostViewDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            }).FirstOrDefaultAsync();
}
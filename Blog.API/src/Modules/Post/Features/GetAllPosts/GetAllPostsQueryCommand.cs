using System.Linq.Dynamic.Core;
using Blog.API.Core.Dto;
using Blog.API.Core.Paginator;
using Blog.API.Data;
using Blog.API.Modules.Post.Controllers.ViewDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Modules.Post.Features.GetAllPosts;

public class GetAllPostsQueryCommand(AppDbContext context) : IRequestHandler<GetAllPostsQuery, Paginator<PostViewDto>>
{
    public async Task<Paginator<PostViewDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var param = request.queryParams;
        
        var query = context.Posts.AsQueryable();
            
        if (!string.IsNullOrWhiteSpace(param.TitleSearchTerm))
        {
            query = query.Where(p => p.Title.Contains(param.TitleSearchTerm));
        }
            
        var sortDirection = param.SortDirection == SortOrder.Asc ? "asc" : "desc";
            
        var sortBy = string.IsNullOrWhiteSpace(param.SortBy)
            ? "Id"
            : param.SortBy;
            
        query = query.OrderBy($"{sortBy} {sortDirection}");
            
            
        var totalCount = await query.CountAsync(cancellationToken);
            
            
        var result = await query
            .Skip((param.PageNumber - 1) * param.PageSize)
            .Take(param.PageSize)
            .Select(post => new PostViewDto
            
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            }).ToListAsync(cancellationToken);
            
        return new Paginator<PostViewDto>
        {
            PageNumber = param.PageNumber,
            PageSize = result.Count,
            TotalItemsCount = totalCount,
            Items = result
        };
        
    }
}
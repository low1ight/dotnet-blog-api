using Blog.API.Core.Paginator;
using Blog.API.Modules.Post.Controllers.InputDto;
using Blog.API.Modules.Post.Controllers.ViewDto;
using MediatR;

namespace Blog.API.Modules.Post.Features.GetAllPosts;

public record GetAllPostsQuery(PostQueryParams queryParams) : IRequest<Paginator<PostViewDto>>;
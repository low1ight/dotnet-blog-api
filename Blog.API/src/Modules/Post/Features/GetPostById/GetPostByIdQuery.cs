using Blog.API.Modules.Post.Controllers.ViewDto;
using MediatR;

namespace Blog.API.Modules.Post.Features.GetPostById;

public record GetPostByIdQuery(int id) : IRequest<PostViewDto?>;
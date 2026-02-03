using MediatR;

namespace Blog.API.Modules.Post.Features.DeletePostById;

public record DeletePostByIdCommand(int id) : IRequest<bool>;
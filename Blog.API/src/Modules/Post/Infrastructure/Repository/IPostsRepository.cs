namespace Blog.API.Modules.Post.Infrastructure.Repository;

public interface IPostsRepository
{
    Task<bool> CreatePost(Domain.Post post);
    Task<bool> UpdatePost(Domain.Post post);
    Task<bool> DeletePost(Domain.Post post);
    Task<Domain.Post?> GetPostById(int id);
}
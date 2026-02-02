using Microsoft.EntityFrameworkCore;
using Blog.API.Modules.Post.Domain;


namespace Blog.API.Data;



public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
}
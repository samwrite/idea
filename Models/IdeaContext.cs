using Microsoft.EntityFrameworkCore;
 
namespace idea.Models
{
    public class IdeaContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public IdeaContext(DbContextOptions<IdeaContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<Idea> Ideas { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entity.User> User { get; set; }
    }
}

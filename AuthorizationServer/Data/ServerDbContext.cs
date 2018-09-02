using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthorizationServer.Entity;

namespace AuthorizationServer.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext (DbContextOptions<ServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuthorizationServer.Entity.User> User { get; set; }
    }
}

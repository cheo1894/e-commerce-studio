using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Infrastructure.Persistence
{


    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}
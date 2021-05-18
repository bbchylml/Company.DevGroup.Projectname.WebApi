using Company.DevGroup.Projectname.IdentityServer4.Domain;
using Microsoft.EntityFrameworkCore;

namespace Company.DevGroup.Projectname.IdentityServer4.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

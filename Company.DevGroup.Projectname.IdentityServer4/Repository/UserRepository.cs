using Company.DevGroup.Projectname.IdentityServer4.Context;
using Company.DevGroup.Projectname.IdentityServer4.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.IdentityServer4.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserDbContext _dbContext;
        private readonly DbSet<User> _dbSet;
        private readonly string _connStr;

        public UserRepository(UserDbContext dbcontext)
        {
            this._dbContext = dbcontext;
            this._dbSet = _dbContext.Set<User>();
            this._connStr = _dbContext.Database.GetDbConnection().ConnectionString;
        }


        public async Task<User> FindAsync(string userName)
        {
            return await _dbContext.Users.SingleAsync(p => p.UserName == userName);
        }
    }
}

using Company.DevGroup.Projectname.IdentityServer4.Domain;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.IdentityServer4.Repository
{
    public interface IUserRepository
    {
        Task<User> FindAsync(string userName);
    }
}

using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models;
using System;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.Application
{
    public interface IUserApp
    {
        bool IsValid(LoginRequestDTO req);
        Users FetchAsync(Func<Users, bool> userInfo);
    }
}

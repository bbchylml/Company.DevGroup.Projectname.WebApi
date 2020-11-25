using System;
using Microsoft.AspNetCore.Identity;

namespace Company.DevGroup.Projectname.Models
{
    public class Users : IdentityUser<int>
    {
        public Users()
        {
        }
        public Users(string userName)
        {
            base.UserName = userName;
        }

        public DateTime CreatedTime { get; set; }

        public bool IsDisabled { get; set; }
    }
}

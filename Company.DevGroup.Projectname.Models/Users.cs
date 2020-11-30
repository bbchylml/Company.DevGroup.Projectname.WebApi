<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6

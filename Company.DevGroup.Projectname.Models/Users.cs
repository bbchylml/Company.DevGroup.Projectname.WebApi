<<<<<<< HEAD
<<<<<<< HEAD
﻿using System;
=======
﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
>>>>>>> add Identity Server4
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
<<<<<<< HEAD
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
=======
        public List<Claim> Claims { get; set; }
    }
}
>>>>>>> add Identity Server4

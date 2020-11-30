<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> add Identity Server4
﻿using Company.DevGroup.Projectname.Application.Dtos;
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
<<<<<<< HEAD
=======
﻿using Company.DevGroup.Projectname.Application.Dtos;
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
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6
=======
>>>>>>> add Identity Server4

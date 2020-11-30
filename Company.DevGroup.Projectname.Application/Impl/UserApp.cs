<<<<<<< HEAD
﻿using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.Application.Impl
{
    public class UserApp : IUserApp
    {

        //模拟测试，默认都是人为验证有效
        public bool IsValid(LoginRequestDTO req)
        {
            return true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>

        public Users FetchAsync(Func<Users, bool> userInfo)
        {
            return new Users();
        }
    }
}
=======
﻿using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.Application.Impl
{
    public class UserApp : IUserApp
    {

        //模拟测试，默认都是人为验证有效
        public bool IsValid(LoginRequestDTO req)
        {
            return true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>

        public Users FetchAsync(Func<Users, bool> userInfo)
        {
            return new Users();
        }
    }
}
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6

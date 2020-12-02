using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models;
using System;

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
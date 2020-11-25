using System;
using System.Collections.Generic;
using System.Text;

namespace Company.DevGroup.Projectname.Application.Dtos
{
    /// <summary>
    /// UserTokenDTO
    /// </summary>
    public class UserTokenDTO : TokenEntityDTO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
    }
}

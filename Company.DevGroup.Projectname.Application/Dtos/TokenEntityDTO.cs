using System;
using System.Collections.Generic;
using System.Text;

namespace Company.DevGroup.Projectname.Application.Dtos
{
    /// <summary>
    /// TokenEntityDTO
    /// </summary>
    public class TokenEntityDTO
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// ExpiresIn
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}

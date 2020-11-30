<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> add Identity Server4
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.DevGroup.Projectname.WebApi.Controllers.v2
{
    /// <summary>
    /// Values v2
    /// </summary>
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Get Values
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return "values2";
        }
    }
}
<<<<<<< HEAD
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.DevGroup.Projectname.WebApi.Controllers.v2
{
    /// <summary>
    /// Values v2
    /// </summary>
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Get Values
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            return "values2";
        }
    }
}
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6
=======
>>>>>>> add Identity Server4

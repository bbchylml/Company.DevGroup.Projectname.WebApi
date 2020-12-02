using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.DevGroup.Projectname.WebApi.Controllers.v2
{
    /// <summary>
    /// Values v2
    /// </summary>
    [ApiVersion("2")]
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
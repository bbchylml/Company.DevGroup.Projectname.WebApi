using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.DevGroup.Projectname.WebApi.Controllers.v1
{
    /// <summary>
    /// Values v1
    /// </summary>
    [ApiVersion("1", Deprecated = true)]
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
            return HttpContext.GetRequestedApiVersion().ToString();
        }
    }
}
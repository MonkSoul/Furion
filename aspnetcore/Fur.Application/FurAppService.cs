using Fur.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Fur.Application
{
    [Route("api/[controller]")]
    [Route("api/[controller]/second")]
    [Route("api/[controller]/three")]
    public class FurAppService : IDynamicApiController
    {
        [HttpGet]
        [HttpGet("get/[action]")]
        [HttpPost]
        [HttpPost("post/cus-version")]
        public string GetVersion()
        {
            return "1.0.0";
        }
    }
}
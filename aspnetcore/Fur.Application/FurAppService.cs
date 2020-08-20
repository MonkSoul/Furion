using Fur.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Fur.Application
{
    [Route("customapi/mobile/[controller]")]
    public class FurAppService : IDynamicApiController
    {
        [Route("get/[action]")]
        public string GetVersion()
        {
            return "1.0.0";
        }
    }
}
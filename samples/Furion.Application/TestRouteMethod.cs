using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Furion.Application
{
    [Route("sys"), ApiDescriptionSettings(ForceWithRoutePrefix = true)]
    public class TestRouteMethod : IDynamicApiController
    {
        [HttpGet("getDesc")]
        public string GetDescription()
        {
            return "Furion";
        }

        public string GetDescription2()
        {
            return "Furion";
        }
    }
}

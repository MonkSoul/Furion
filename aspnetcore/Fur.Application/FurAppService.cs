using Fur.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        [HttpPost, HttpGet, AcceptVerbs("PUT", "DELETE")]
        public string GetVersion()
        {
            return "1.0.0";
        }
    }
}
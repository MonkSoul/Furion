using Fur.LazyController;
using Microsoft.AspNetCore.Mvc;

namespace Fur.Application
{
    [Route("api/[controller]")]
    [Route("api/[controller]/22")]
    //[ApiDescriptionSettings(Module = "mobile", ApiVersion = "2.0")]
    public class FurAppService : ILazyController
    {
        //[ApiDescriptionSettings(false)]
        //[ApiDescriptionSettings(ApiVersion = "2.0")]
        [HttpGet]
        [HttpGet("dfd")]
        [HttpGet("dfd222")]
        [AcceptVerbs("POST")]
        public string Get()
        {
            return "v1.0.0";
        }

        ////[HttpGet("[action]")]
        ////[Route("/[action]")]
        //[ApiDescriptionSettings(SplitName = true, ApiVersion = "1.0")]
        //public string GetFrameworkName(string name)
        //{
        //    return nameof(Fur) + name;
        //}

        //[ApiDescriptionSettings(false)]
        [HttpPost("dfdfd")]
        public string Post([FromBody] TestDto dto)
        {
            return dto.name;
        }
    }
}
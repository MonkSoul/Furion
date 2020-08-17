using Fur.DynamicApiController;

namespace Fur.Application
{
    //[Route("api/[controller]")]
    //[Route("api/[controller]/22")]
    //[ApiController]
    //[ApiDescriptionSettings(Module = "mobile", Version = "2.0")]
    public class FurAppService : IDynamicApiController
    {
        ////[ApiDescriptionSettings(false)]
        //[ApiDescriptionSettings(Version = "2.0", KeepVerb = true)]
        ////[HttpGet]
        ////[HttpGet("dfd")]
        ////[HttpGet("dfd222")]
        ////[AcceptVerbs("POST")]
        //public string Get()
        //{
        //    return "v1.0.0";
        //}

        ////[HttpGet("[action]")]
        ////[Route("/[action]")]
        //[ApiDescriptionSettings(SplitName = true, ApiVersion = "1.0")]
        //public string GetFrameworkName(string name)
        //{
        //    return nameof(Fur) + name;
        //}

        ////[HttpGet]
        ////[AcceptVerbs("POST")]
        //[ApiDescriptionSettings(KeepVerb = true, Version = "2.0", SplitCamelCase = true)]
        //[HttpPost]
        //[HttpGet]
        //[HttpGet("dfdfd")]
        public string PostName([ApiSeat(ApiSeats.ControllerStart)] int id, string myName, TestDto dto)
        {
            return dto.name + myName;
        }
    }
}
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
        public string Get()
        {
            return $"Hello {nameof(Fur)}";
        }

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
            return dto.Name + myName;
        }

        public string PostNameV2([ApiSeat(ApiSeats.ControllerStart)] int id, string myName, TestDto dto)
        {
            return dto.Name + myName;
        }

        public string PostNameV2_1_2([ApiSeat(ApiSeats.ControllerStart)] int id, string myName, TestDto dto)
        {
            return dto.Name + myName;
        }

        [ApiDescriptionSettings(Version = "3.0")]
        public string PostNameV2_1_3([ApiSeat(ApiSeats.ControllerStart)] int id, string myName, TestDto dto)
        {
            return dto.Name + myName;
        }
    }
}
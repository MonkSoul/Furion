using Fur.Application;
using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/[controller]/22")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //[HttpGet]
        //[HttpGet("/dfd/")]
        //[HttpGet("dfd222")]
        //[AcceptVerbs("POST")]
        //public string Get()
        //{
        //    return "1";
        //}

        [HttpPost]
        //[HttpGet]
        //[HttpGet("dfdfd")]
        //[AcceptVerbs("POST")]
        public string Post(TestDto dto)
        {
            return dto.name;
        }
    }
}
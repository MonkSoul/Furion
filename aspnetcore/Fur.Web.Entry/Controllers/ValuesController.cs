using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/22")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [HttpGet("dfd")]
        [AcceptVerbs("POST")]
        public string Get()
        {
            return "1";
        }
    }
}
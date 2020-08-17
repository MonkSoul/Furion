using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    public class FurController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return nameof(Fur);
        }
    }
}
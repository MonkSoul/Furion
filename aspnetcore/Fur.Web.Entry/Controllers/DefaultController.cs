using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    [Route("api/[controller]")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return $"Hello {nameof(Fur)}";
        }
    }
}
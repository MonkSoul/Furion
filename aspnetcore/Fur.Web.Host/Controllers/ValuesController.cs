using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fur.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Content(nameof(Fur)));
        }
    }
}

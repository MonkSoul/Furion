using Microsoft.AspNetCore.Mvc;

namespace Fur.Web.Entry.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
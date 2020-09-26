using Fur.Web.MvcSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fur.Application;

namespace Fur.Web.MvcSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMvcService _mvcService;

        public HomeController(ILogger<HomeController> logger, IMvcService mvcService)
        {
            _logger = logger;
            _mvcService = mvcService;
        }

        public IActionResult Index()
        {
            ViewBag.County = _mvcService.GetAll().Result.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

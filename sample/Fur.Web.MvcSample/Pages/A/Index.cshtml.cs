using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fur.Application;

namespace Fur.Web.MvcSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMvcService _mvcService;
        public IndexModel(ILogger<IndexModel> logger, IMvcService mvcService)
        {
            _logger = logger;
            _mvcService = mvcService;
        }

        public int Count;
        public void OnGet()
        {
            Count = _mvcService.GetAll().Result.Count();
        }
    }
}

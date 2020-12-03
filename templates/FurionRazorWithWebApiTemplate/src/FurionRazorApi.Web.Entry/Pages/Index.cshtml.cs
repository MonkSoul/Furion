using FurionRazorApi.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurionRazorApi.Web.Entry.Pages
{
    public class IndexModel : PageModel
    {
        public readonly ISystemService _systemService;

        public IndexModel(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public void OnGet()
        {
            ViewData["Description"] = _systemService.GetDescription();
        }
    }
}
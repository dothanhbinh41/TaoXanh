using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaoxanhProxy.Services;

namespace TaoXanhProxyWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IInternalService authService;

        public IndexModel(ILogger<IndexModel> logger, IInternalService authService)
        {
            _logger = logger;
            this.authService = authService;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await authService.GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToPage("./Login");
            }
            return RedirectToRoute("./Profile");
        }
    }
}
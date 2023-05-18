using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaoxanhProxy.Models;
using TaoxanhProxy.Services;

namespace TaoxanhProxy.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IInternalService authService;

        [BindProperty]
        public UserModel? UserModel { get; set; }
        public ProfileModel(IInternalService authService)
        {
            this.authService = authService;
        }

        public async Task OnGet()
        {
            UserModel = await authService.GetCurrentUserAsync();
            if (UserModel == null)
            {
                Response.Redirect("./Login");
            }
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await authService.LogoutAsync();
            return RedirectToPage("./Login");
        }
    }
}

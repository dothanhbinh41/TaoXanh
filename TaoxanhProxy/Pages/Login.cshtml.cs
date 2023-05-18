using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaoxanhProxy.Services;

namespace TaoxanhProxy.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        private readonly IInternalService authService;

        public LoginModel(IInternalService authService)
        {
            this.authService = authService;
        }


        public void OnGet()
        {
            
        }
        public async Task OnPostLogin()
        {
            var res = await authService.AuthAsync(Username, Password);
            if (res)
            {
                ErrorMessage = string.Empty;
                Response.Redirect("./Profile");
            }
            ErrorMessage = "Incorect username or password";
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NintendoInventory.UI.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        { 
            await HttpContext.SignOutAsync("NintendoInventroyCookie");
            return RedirectToPage("/Index");
        }
    }
}

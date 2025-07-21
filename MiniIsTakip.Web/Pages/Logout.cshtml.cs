using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniIsTakip.Web.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Oturumu temizle
            HttpContext.Session.Clear();

            // Login sayfas�na y�nlendir
            return RedirectToPage("Login");
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MiniIsTakip.Web.Pages
{
    public class AnaSayfaModel : PageModel
    {
        public string UserFullName { get; set; } = string.Empty;

        public void OnGet()
        {
            UserFullName = HttpContext.Session.GetString("UserFullName") ?? "Kullanýcý";
        }
    }
}

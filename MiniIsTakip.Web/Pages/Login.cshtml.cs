using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniIsTakip.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniIsTakip.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string FullName { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Form ge�erli de�il. L�tfen t�m alanlar� do�ru �ekilde doldurun.";
                return Page();
            }

            var user = await _context.Persons
                .FirstOrDefaultAsync(p => p.Email == Email && p.FullName == FullName);

            if (user == null)
            {
                ErrorMessage = "Kullan�c� bulunamad�. L�tfen bilgileri kontrol edin.";
                return Page();
            }

            // Kullan�c�y� oturumda sakla
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserFullName", user.FullName);

            /*if (user.Role == "Yonetici")
                return RedirectToPage("/TaskCreate");
            else
                return RedirectToPage("/TaskList");*/
            if (user.Role == "Yonetici")
                return RedirectToPage("/AnaSayfa");
            else
                return RedirectToPage("/TaskList");

        }
    }
}

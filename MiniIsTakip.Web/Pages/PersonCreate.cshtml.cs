using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniIsTakip.Core.Entities;
using MiniIsTakip.Infrastructure.Data;
using System.Threading.Tasks;

namespace MiniIsTakip.Web.Pages
{
    public class PersonCreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public PersonCreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; }

        public void OnGet()
        {
            // Boþ býrakabiliriz, form dolacak
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Yeni çalýþan rolü: "Calisan"
            Person.Role = "Calisan";
            Person.IsActive = true;

            _context.Persons.Add(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AnaSayfa");
        }
    }
}

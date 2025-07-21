using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniIsTakip.Core.Entities;
using MiniIsTakip.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniIsTakip.Web.Pages
{
    public class TaskCreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public TaskCreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        // �al��an listesini dropdown i�in tutuyoruz
        public List<Person> Employees { get; set; }

        public async Task OnGetAsync()
        {
            // Sadece �al��an rol�ndeki kullan�c�lar� al�yoruz
            Employees = await _context.Persons
                .Where(p => p.Role == "Calisan")
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();

                // Hatalar� debug penceresine yazd�r
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine("Validation Error: " + error);
                }

                Employees = await _context.Persons
                    .Where(p => p.Role == "Calisan")
                    .ToListAsync();

                return Page();
            }

            try
            {
                _context.Tasks.Add(TaskItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Kay�t s�ras�nda hata olu�tu: " + ex.Message);
                Employees = await _context.Persons
                    .Where(p => p.Role == "Calisan")
                    .ToListAsync();
                return Page();
            }

            return RedirectToPage("TaskList");
        }

        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Formda hata varsa �al��an listesini tekrar doldur
                Employees = await _context.Persons
                    .Where(p => p.Role == "Calisan")
                    .ToListAsync();
                return Page();
            }

            _context.Tasks.Add(TaskItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("TaskList"); // �� listesine y�nlendir
        }*/
    }
}

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

        // Çalýþan listesini dropdown için tutuyoruz
        public List<Person> Employees { get; set; }

        public async Task OnGetAsync()
        {
            // Sadece çalýþan rolündeki kullanýcýlarý alýyoruz
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

                // Hatalarý debug penceresine yazdýr
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
                ModelState.AddModelError(string.Empty, "Kayýt sýrasýnda hata oluþtu: " + ex.Message);
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
                // Formda hata varsa çalýþan listesini tekrar doldur
                Employees = await _context.Persons
                    .Where(p => p.Role == "Calisan")
                    .ToListAsync();
                return Page();
            }

            _context.Tasks.Add(TaskItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("TaskList"); // Ýþ listesine yönlendir
        }*/
    }
}

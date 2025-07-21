using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniIsTakip.Core.Entities;
using MiniIsTakip.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniIsTakip.Web.Pages
{
    public class TaskListModel : PageModel
    {
        private readonly AppDbContext _context;

        public TaskListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public async Task OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (role == "Calisan" && userId.HasValue)
            {
                Tasks = await _context.Tasks
                    .Include(t => t.AssignedPerson)
                    .Where(t => t.AssignedPersonId == userId.Value)
                    .ToListAsync();
            }
            else
            {
                // Yönetici veya diðer roller için tüm görevler
                Tasks = await _context.Tasks
                    .Include(t => t.AssignedPerson)
                    .ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostCompleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

    }
}

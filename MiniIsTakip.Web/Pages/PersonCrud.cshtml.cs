using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniIsTakip.Core.Entities;
using MiniIsTakip.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PersonCrudModel : PageModel
{
    private readonly AppDbContext _context;

    public PersonCrudModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Person> People { get; set; } = new();

    public async Task OnGetAsync()
    {
        People = await _context.Persons.ToListAsync();
    }
}

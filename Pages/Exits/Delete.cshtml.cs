using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Exits
{
    public class DeleteModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DeleteModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Exit Exit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exit = await _context.Exits.FirstOrDefaultAsync(m => m.Id == id);

            if (exit is not null)
            {
                Exit = exit;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exit = await _context.Exits.FindAsync(id);
            if (exit != null)
            {
                Exit = exit;
                _context.Exits.Remove(Exit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

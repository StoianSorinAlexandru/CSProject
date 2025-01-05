using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.DetailedEntries
{
    public class DeleteModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DeleteModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DetailedEntry DetailedEntry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailedentry = await _context.DetailedEntries.FirstOrDefaultAsync(m => m.Id == id);

            if (detailedentry is not null)
            {
                DetailedEntry = detailedentry;

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

            var detailedentry = await _context.DetailedEntries.FindAsync(id);
            if (detailedentry != null)
            {
                DetailedEntry = detailedentry;
                _context.DetailedEntries.Remove(DetailedEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

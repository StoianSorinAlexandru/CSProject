using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.DetailedExits
{
    public class DeleteModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DeleteModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DetailedExit DetailedExit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detailedexit = await _context.DetailedExits.FirstOrDefaultAsync(m => m.Id == id);

            if (detailedexit is not null)
            {
                DetailedExit = detailedexit;

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

            var detailedexit = await _context.DetailedExits.FindAsync(id);
            if (detailedexit != null)
            {
                DetailedExit = detailedexit;
                _context.DetailedExits.Remove(DetailedExit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

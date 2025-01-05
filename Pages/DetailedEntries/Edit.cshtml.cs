using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.DetailedEntries
{
    public class EditModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public EditModel(miniProiect2.Data.miniProiect2Context context)
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

            var detailedentry =  await _context.DetailedEntries.FirstOrDefaultAsync(m => m.Id == id);
            if (detailedentry == null)
            {
                return NotFound();
            }
            DetailedEntry = detailedentry;
           ViewData["Id"] = new SelectList(_context.Entries, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DetailedEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailedEntryExists(DetailedEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DetailedEntryExists(int id)
        {
            return _context.DetailedEntries.Any(e => e.Id == id);
        }
    }
}

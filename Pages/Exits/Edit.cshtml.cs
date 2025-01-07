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

namespace miniProiect2.Pages.Exits
{
    public class EditModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public EditModel(miniProiect2.Data.miniProiect2Context context)
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

            var exit =  await _context.Exits.FirstOrDefaultAsync(m => m.Id == id);
            if (exit == null)
            {
                return NotFound();
            }
            Exit = exit;
           ViewData["GestionId"] = new SelectList(_context.Gestions, "Id", "Id");
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

            _context.Attach(Exit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExitExists(Exit.Id))
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

        private bool ExitExists(int id)
        {
            return _context.Exits.Any(e => e.Id == id);
        }
    }
}

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

namespace miniProiect2.Pages.DetailedExits
{
    public class EditModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public EditModel(miniProiect2.Data.miniProiect2Context context)
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

            var detailedexit =  await _context.DetailedExits.FirstOrDefaultAsync(m => m.Id == id);
            if (detailedexit == null)
            {
                return NotFound();
            }
            DetailedExit = detailedexit;
           ViewData["Id"] = new SelectList(_context.Exits, "Id", "Id");
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

            _context.Attach(DetailedExit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailedExitExists(DetailedExit.Id))
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

        private bool DetailedExitExists(int id)
        {
            return _context.DetailedExits.Any(e => e.Id == id);
        }
    }
}

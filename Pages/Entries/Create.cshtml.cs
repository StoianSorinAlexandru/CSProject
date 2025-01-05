using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public CreateModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GestionId"] = new SelectList(_context.Set<Gestion>(), "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var partnerQuery = from m in _context.Partners
                               where m.Id == Entry.PartnerId
                               select m;
            var partnerResult = await partnerQuery.FirstOrDefaultAsync();
            if (partnerResult != null)
            {
                Entry.Partner = partnerResult;
                Entry.PartnerId = partnerResult.Id;
            }
            var gestionQuery = from m in _context.Gestions
                               where m.Id == Entry.GestionId
                               select m;
            var gestionResult = await gestionQuery.FirstOrDefaultAsync();
            if (gestionResult != null)
            {
                Entry.Gestion = gestionResult;
                Entry.GestionId = gestionResult.Id;
            }

            _context.Entries.Add(Entry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

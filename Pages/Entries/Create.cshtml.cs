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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        [BindProperty]
        public DetailedEntry DetailedEntry { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Cream noua intrare
            var partnerQuery = from p in _context.Partners
                               where p.Id == Entry.PartnerId
                               select p;
            var partnerResult = await partnerQuery.FirstOrDefaultAsync();
            if (partnerResult != null)
            {
                Entry.Partner = partnerResult;
                Entry.PartnerId = partnerResult.Id;
            }

            var gestionQuery = from g in _context.Gestions
                               where g.Id == Entry.GestionId
                               select g;
            var gestionResult = await gestionQuery.FirstOrDefaultAsync();
            if (gestionResult != null)
            {
                Entry.Gestion = gestionResult;
                Entry.GestionId = gestionResult.Id;
            }

            _context.Entries.Add(Entry);
            await _context.SaveChangesAsync();

            //Dupa ce a fost creata cream un DetailedEntry la care ii atasam intrarea

            var productQuery = from p in _context.Products
                               where p.Id == DetailedEntry.ProductId
                               select p;
            var productResult = await productQuery.FirstOrDefaultAsync();
            if(productResult != null)
            {
                DetailedEntry.Product = productResult;
                DetailedEntry.ProductId = productResult.Id;
                DetailedEntry.Entry = Entry;
                DetailedEntry.EntryId = Entry.Id;
            }
            _context.DetailedEntries.Add(DetailedEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

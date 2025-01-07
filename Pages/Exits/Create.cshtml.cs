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
using Newtonsoft.Json;

namespace miniProiect2.Pages.Exits
{
    public class CreateModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public CreateModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Exit Exit { get; set; } = default!;

        [BindProperty]
        public Entry SelectedEntry { get; set; } = default!;
        [BindProperty]
        public DetailedEntry detailedEntry{ get; set; } = default!;

        [BindProperty]
        public DetailedExit DetailedExit { get; set; } = default!;

        [BindProperty]
        public bool isSelected { get; set; } = false!;

        public IActionResult OnGet()
        {
            ViewData["GestionId"] = new SelectList(_context.Gestions, "Id", "Name");
            var gestionsQuery = from g in _context.Gestions
                                join e in (
                                    from e in _context.Entries
                                    join de in _context.DetailedEntries on e.Id equals de.EntryId
                                    select e
                                ) on g.Id equals e.GestionId
                                select g;

            var productsQuery = from p in _context.Products
                                join de in _context.DetailedEntries on p.Id equals de.ProductId
                                select p;
            var entriesQuery = from e in _context.Entries
                               select e;

            ViewData["ProductId"] = new SelectList(productsQuery, "Id", "Name");
            ViewData["GestionId"] = new SelectList(gestionsQuery, "Id", "Name");
            ViewData["EntryId"] = new SelectList(entriesQuery, "Id", "Id");
            if (TempData["SelectedEntry"] != null)
            {
                isSelected = true;
                SelectedEntry = JsonConvert.DeserializeObject<Entry>((string)TempData["SelectedEntry"]);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSelectEntryAsync()
        {

            detailedEntry = await _context.DetailedEntries
                .Include(d => d.Product)
                .Include(e => e.Entry)
                .FirstOrDefaultAsync(m => m.EntryId == SelectedEntry.Id);

            SelectedEntry = await _context.Entries
                .Include(p => p.Partner)
                .Include(g => g.Gestion)
                .FirstOrDefaultAsync(e => e.Id == detailedEntry.EntryId);

            if (SelectedEntry != null)
            {
                TempData["SelectedEntry"] = JsonConvert.SerializeObject(SelectedEntry);
            }

            return RedirectToPage();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Exits.Add(Exit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public IList<Entry> Entry { get; set; } = default!;
        [BindProperty]
        public Entry SelectedEntry { get; set; } = default!;
        [BindProperty]
        public DetailedEntry detailedEntry { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Retrieve the detailedEntry from TempData if available
            if (TempData["DetailedEntry"] != null)
            {
                detailedEntry = JsonConvert.DeserializeObject<DetailedEntry>((string)TempData["DetailedEntry"]);
            }

            // Fetch entries for the page
            Entry = await _context.Entries
                .Include(e => e.Gestion)
                .Include(e => e.Partner)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostSelectAsync(int Id)
        {
            // Fetch the selected entry
            SelectedEntry = 
                await _context.Entries
                .Include(e => e.Gestion)
                .Include(e => e.Partner)
                .FirstOrDefaultAsync(m => m.Id == Id);

            // Fetch the detailed entry associated with the selected entry
            detailedEntry = await _context.DetailedEntries
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.EntryId == Id);

            // Store detailedEntry in TempData for persistence
            if (detailedEntry != null)
            {
                TempData["DetailedEntry"] = JsonConvert.SerializeObject(detailedEntry);
            }

            return RedirectToPage();
        }
    }
}

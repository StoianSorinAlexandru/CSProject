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
    public class IndexModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public IList<DetailedEntry> DetailedEntry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DetailedEntry = await _context.DetailedEntries
                .Include(d => d.Entry)
                .Include(d => d.Product).ToListAsync();
        }
    }
}

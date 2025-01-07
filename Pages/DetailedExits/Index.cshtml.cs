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
    public class IndexModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public IList<DetailedExit> DetailedExit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DetailedExit = await _context.DetailedExits
                .Include(d => d.Exit)
                .Include(d => d.Product).ToListAsync();
        }
    }
}

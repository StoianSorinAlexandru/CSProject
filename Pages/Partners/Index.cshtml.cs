using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Partners
{
    public class IndexModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public IndexModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public IList<Partner> Partner { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Partner = await _context.Partners.ToListAsync();
        }
    }
}

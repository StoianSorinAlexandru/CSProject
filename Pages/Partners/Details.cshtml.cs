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
    public class DetailsModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DetailsModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public Partner Partner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners.FirstOrDefaultAsync(m => m.Id == id);

            if (partner is not null)
            {
                Partner = partner;

                return Page();
            }

            return NotFound();
        }
    }
}

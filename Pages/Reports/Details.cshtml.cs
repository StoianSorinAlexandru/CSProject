using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Reports
{
    public class DetailsModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DetailsModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        public Models.Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FirstOrDefaultAsync(m => m.Id == id);

            if (report is not null)
            {
                Report = report;

                return Page();
            }

            return NotFound();
        }
    }
}

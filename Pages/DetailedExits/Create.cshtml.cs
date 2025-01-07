using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.DetailedExits
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
        ViewData["ExitId"] = new SelectList(_context.Exits, "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DetailedExit DetailedExit { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DetailedExits.Add(DetailedExit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

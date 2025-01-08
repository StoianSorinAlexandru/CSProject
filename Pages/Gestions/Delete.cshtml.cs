﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Data;
using miniProiect2.Models;

namespace miniProiect2.Pages.Gestions
{
    public class DeleteModel : PageModel
    {
        private readonly miniProiect2.Data.miniProiect2Context _context;

        public DeleteModel(miniProiect2.Data.miniProiect2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Gestion Gestion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestion = await _context.Gestions.FirstOrDefaultAsync(m => m.Id == id);

            if (gestion is not null)
            {
                Gestion = gestion;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestion = await _context.Gestions.FindAsync(id);
            if (gestion != null)
            {
                Gestion = gestion;
                _context.Gestions.Remove(Gestion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
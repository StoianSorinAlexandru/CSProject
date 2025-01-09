using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using miniProiect2.Models;

namespace miniProiect2.Data
{
    public class miniProiect2Context : DbContext
    {
        public miniProiect2Context (DbContextOptions<miniProiect2Context> options)
            : base(options)
        {
        }

        public DbSet<miniProiect2.Models.Entry> Entries { get; set; } = default!;
        public DbSet<miniProiect2.Models.Gestion> Gestions { get; set; } = default!;
        public DbSet<miniProiect2.Models.Partner> Partners { get; set; } = default!;
        public DbSet<miniProiect2.Models.Product> Products { get; set; } = default!;
        public DbSet<miniProiect2.Models.DetailedEntry> DetailedEntries { get; set; } = default!;
        
        public DbSet<miniProiect2.Models.Exit> Exits { get; set; } = default!;

        public DbSet<miniProiect2.Models.DetailedExit> DetailedExits { get; set; }

        public DbSet<miniProiect2.Models.DetailedEntry> DetailedEntry { get; set; }
        public DbSet<miniProiect2.Models.Report> Reports { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using miniProiect2.Models;

namespace miniProiect2.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new miniProiect2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<miniProiect2Context>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null miniProiect2Context");
                }

                // Check if any data already exists.
                if (context.Products.Any() || context.Partners.Any() ||
                    context.Gestions.Any() || context.Entries.Any() || context.Exits.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed Products
                var products = new[]
                {
                    new Product { Name = "Product A", Price = 10.5f },
                    new Product { Name = "Product B", Price = 15.75f },
                    new Product { Name = "Product C", Price = 7.30f }
                };
                context.Products.AddRange(products);
                context.SaveChanges();

                // Seed Partners
                var partners = new[]
                {
                    new Partner { Name = "Partner A", CUI = "123456", Address = "123 Street A" },
                    new Partner { Name = "Partner B", CUI = "654321", Address = "456 Street B" }
                };
                context.Partners.AddRange(partners);
                context.SaveChanges();

                // Seed Gestions
                var gestions = new[]
                {
                    new Gestion { Name = "Gestion A" },
                    new Gestion { Name = "Gestion B" }
                };
                context.Gestions.AddRange(gestions);
                context.SaveChanges();

                // Seed Entries with navigation properties
                var entries = new[]
                {
                    new Entry
                    {
                        Date = DateTime.Parse("2023-01-01"),
                        Partner = partners[0], // Assign Partner object
                        Gestion = gestions[0]  // Assign Gestion object
                    },
                    new Entry
                    {
                        Date = DateTime.Parse("2023-02-01"),
                        Partner = partners[1], // Assign Partner object
                        Gestion = gestions[1]  // Assign Gestion object
                    }
                };
                context.Entries.AddRange(entries);
                context.SaveChanges();

                var detailedEntries = new[]
                {
                    new DetailedEntry
                    {
                        Entry = context.Entries.AsNoTracking().First(e => e.Id == entries[0].Id),
                        Product = context.Products.AsNoTracking().First(p => p.Id == products[0].Id),
                        Quantity = 10
                    },
                    new DetailedEntry
                    {
                        Entry = context.Entries.AsNoTracking().First(e => e.Id == entries[0].Id),
                        Product = context.Products.AsNoTracking().First(p => p.Id == products[1].Id),
                        Quantity = 5
                    },
                    new DetailedEntry
                    {
                        Entry = context.Entries.AsNoTracking().First(e => e.Id == entries[1].Id),
                        Product = context.Products.AsNoTracking().First(p => p.Id == products[2].Id),
                        Quantity = 7
                    }
                };
                context.DetailedEntries.AddRange(detailedEntries);
                context.SaveChanges();

                // Seed Detailed Entries



                // Seed Exits
                var exits = new[]
                {
                    new Exit { Date = DateTime.Parse("2023-03-01"), Gestion = gestions[0] },
                    new Exit { Date = DateTime.Parse("2023-04-01"), Gestion = gestions[1] }
                };
                context.Exits.AddRange(exits);
                context.SaveChanges();

                // Seed Detailed Exits
                context.DetailedExits.AddRange(
                    new DetailedExit { Exit = exits[0], Product = products[0] },
                    new DetailedExit { Exit = exits[1], Product = products[2] }
                );

                // Save changes again
                context.SaveChanges();
            }
        }
    }
}

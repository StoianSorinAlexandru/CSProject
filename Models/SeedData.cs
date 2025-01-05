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
                context.Products.AddRange(
                    new Product { Name = "Product A", Price = 10.5f },
                    new Product { Name = "Product B", Price = 15.75f },
                    new Product { Name = "Product C", Price = 7.30f }
                );
                context.SaveChanges();

                // Seed Partners
                context.Partners.AddRange(
                    new Partner { Name = "Partner A", CUI = "123456", Address = "123 Street A" },
                    new Partner { Name = "Partner B", CUI = "654321", Address = "456 Street B" }
                );
                context.SaveChanges();

                // Seed Gestions
                context.Gestions.AddRange(
                    new Gestion { Name = "Gestion A" },
                    new Gestion { Name = "Gestion B" }
                );
                // Save changes to ensure EntryIds are generated
                context.SaveChanges();

                // Seed Entries
                context.Entries.AddRange(
                    new Entry { Date = DateTime.Parse("2023-01-01"), PartnerId = 1, GestionId = 1 },
                    new Entry { Date = DateTime.Parse("2023-02-01"), PartnerId = 2, GestionId = 2 }
                );

                // Save changes to ensure EntryIds are generated
                context.SaveChanges();

                // Seed Detailed Entries
                context.DetailedEntries.AddRange(
                    new DetailedEntry { EntryId = 1, ProductId = 1, Quantity = 10, Price = 10.5f },
                    new DetailedEntry { EntryId = 1, ProductId = 2, Quantity = 5, Price = 15.75f },
                    new DetailedEntry { EntryId = 2, ProductId = 3, Quantity = 7, Price = 7.30f }
                );
                context.SaveChanges();

                // Seed Exits
                context.Exits.AddRange(
                    new Exit { Date = DateTime.Parse("2023-03-01"), GestionId = 3 },
                    new Exit { Date = DateTime.Parse("2023-04-01"), GestionId = 4 }
                );
                context.SaveChanges();

                // Seed Detailed Exits
                context.DetailedExits.AddRange(
                    new DetailedExit { ExitId = 1, ProductId = 1 },
                    new DetailedExit { ExitId = 2, ProductId = 3 }
                );

                // Save changes again
                context.SaveChanges();
            }
        }
    }
}

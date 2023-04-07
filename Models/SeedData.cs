using AfroLatino.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AfroLatino.Models;

public class SeedData
{

    public static void Init()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite("Data Source=app.db");

        using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            // Look for existing content
            if (context.Events.Any())
            {
                return;   // DB already filled
            }

            Event eventTest = new Event
            {
                Title = "soirée SBK",
                Date = new DateTime(2023, 12, 1),
                DescriptionCourte = "Super soirée danses latines sur Bordeaux",
                DescriptionLongue = "Super soirée danses latines sur Bordeaux",
                Lieu = "L'atelier - Merignac",
            };

            Event eventTest2 = new Event
            {
                Title = "soirée SBK bis",
                Date = new DateTime(2023, 12, 8),
                DescriptionCourte = "Super soirée danses latines sur Bordeaux",
                DescriptionLongue = "Super soirée danses latines sur Bordeaux",
                Lieu = "L'atelier - Merignac",
            };
            context.Events.AddRange(eventTest, eventTest2);

            // Commit changes into DB
            context.SaveChanges();
        }
    }
}
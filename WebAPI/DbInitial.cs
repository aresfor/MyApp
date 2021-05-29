
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using System;
using System.Linq;

namespace WebAPI
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();
            string imageURL = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";

            // Look for any students.
            if (context.Songs.Any())
            {
                return;   // DB has been seeded
            }

            var songs = new Song[]
            {
             new Song{Id = 1,Name = "All Along with you", Singer = "EGOIST", Length = "3:44",Image = imageURL },
            new Song { Id = 2, Name = "All Along with you", Singer = "EGOIST", Length = "3:44", Image = imageURL },
                new Song { Id = 2, Name = "All Along with you", Singer = "EGOIST", Length = "3:44", Image = imageURL }
            };

            context.Songs.AddRange(songs);
            context.SaveChanges();

        }
    }
}
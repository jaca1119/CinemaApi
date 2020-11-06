using CinemaApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Util
{
    public class DbSeed
    {
        internal static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Movies.Add(new Models.Movie { Name = "The Shawshank Redemption" });

            context.SaveChanges();
        }
    }
}

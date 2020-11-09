using CinemaApi.Data;
using CinemaApi.Models;
using System;
using System.Collections.Generic;

namespace CinemaApi.Util
{
    public class DbSeed
    {
        internal static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Movies.AddRange(Movies());

            context.SaveChanges();
        }

        private static List<Movie> Movies()
        {
            List<Movie> movies = new List<Movie>();

            Movie shawshank = new Movie
            {
                Title = "The Shawshank Redemption",
                PosterUrl = "https://fwcdn.pl/fpo/10/48/1048/6925401.6.jpg",
                Description = "Escape from prison",
                Category = Category.Action,
                Duration = 118,
                ScreeningTimes = new List<ScreeningTime> { new ScreeningTime { Screening = DateTime.Now.AddDays(1) }, new ScreeningTime { Screening = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(10) } },
                Seats = new List<Seat>
                {
                    new Seat {Column = 0, Row = 0, Status = SeatStatus.Free},
                    new Seat {Column = 1, Row = 0, Status = SeatStatus.Free},
                    new Seat {Column = 0, Row = 1, Status = SeatStatus.Free},
                    new Seat {Column = 1, Row = 0, Status = SeatStatus.Free}
                }
            };
            movies.Add(shawshank);

            Movie starWars = new Movie
            {
                Title = "Star Wars 4",
                PosterUrl = "https://fwcdn.pl/fpo/05/20/520/7403025.6.jpg",
                Description = "Star wars",
                Category = Category.SciFi,
                Duration = 180,
                ScreeningTimes = new List<ScreeningTime> { new ScreeningTime { Screening = DateTime.Now.AddDays(2) }, new ScreeningTime { Screening = DateTime.Now.AddDays(2).AddHours(5).AddMinutes(32) } },
                Seats = new List<Seat>
                {
                    new Seat {Column = 0, Row = 0, Status = SeatStatus.Free},
                    new Seat {Column = 1, Row = 0, Status = SeatStatus.Excluded},
                    new Seat {Column = 2, Row = 0, Status = SeatStatus.Taken},
                    new Seat {Column = 3, Row = 0, Status = SeatStatus.Free}
                }
            };
            movies.Add(starWars);

            return movies;
        }
    }
}

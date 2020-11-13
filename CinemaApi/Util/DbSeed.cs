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
                ScreeningTimes = new List<ScreeningTime>
                { new ScreeningTime { Screening = DateTime.Now.AddDays(1), Rows = GenerateRandomSeats(12, 15) },
                    new ScreeningTime { Screening = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(10), Rows = GenerateRandomSeats(10, 15) }
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
                ScreeningTimes = new List<ScreeningTime>
                {
                    new ScreeningTime { Screening = DateTime.Now.AddDays(2), Rows = GenerateRandomSeats(4, 5) },
                    new ScreeningTime { Screening = DateTime.Now.AddDays(2).AddHours(5).AddMinutes(32), Rows = GenerateRandomSeats(5, 5) }
                }
            };
            movies.Add(starWars);

            return movies;
        }

        private static List<Row> GenerateRandomSeats(int rows, int cols)
        {
            List<Row> generatedRows = new List<Row>();

            Array seatStatuses = Enum.GetValues(typeof(SeatStatus));
            Random random = new Random();

            for (int i = 0; i < rows; i++)
            {
                List<Seat> seats = new List<Seat>();

                for (int j = 0; j < cols; j++)
                {
                    Seat seat = new Seat { Status = (SeatStatus)seatStatuses.GetValue(random.Next(seatStatuses.Length)) };
                    seats.Add(seat);
                }

                generatedRows.Add(new Row { Seats = seats });
            }

            return generatedRows;
        }
    }
}

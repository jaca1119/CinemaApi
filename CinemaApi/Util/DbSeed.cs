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

            context.Movies.AddRange(Movies(context));

            context.SaveChanges();   
        }

        private static List<Movie> Movies(ApplicationDbContext context)
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

            Movie lost = new Movie
            {
                Title = "Lost",
                PosterUrl = "https://fwcdn.pl/fpo/38/34/133834/7177044.6.jpg",
                Description = "Lost",
                Category = Category.Action,
                Duration = 50,
                ScreeningTimes = new List<ScreeningTime>
                {
                    new ScreeningTime { Screening = DateTime.Now, Rows = GeneratePrettySeats(context)}
                }
            };
            movies.Add(lost);

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

        private static List<Row> GeneratePrettySeats(ApplicationDbContext context)
        {
            List<Row> rows = new List<Row>();
            int[][] seats = new int[][]
            {
                new int []{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                new int []{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };

            foreach (var rawRow in seats)
            {
                Row row = new Row();
                List<Seat> rowSeats = new List<Seat>();

                foreach (int seatStatus in rawRow)
                {
                    if (seatStatus == 1)
                    {
                        Seat seat = new Seat { Status = SeatStatus.Free };
                        rowSeats.Add(seat);
                        context.Seats.Add(seat);
                    }
                    else
                    {
                        Seat seat = new Seat { Status = SeatStatus.Excluded };
                        rowSeats.Add(seat);
                        context.Seats.Add(seat);
                    }

                    context.SaveChanges();
                }

                row.Seats = rowSeats;
                rows.Add(row);
            }

            return rows;
        }
    }
}

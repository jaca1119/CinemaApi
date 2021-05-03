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

            List<Hall> halls = AddHalls(context);
            context.Halls.AddRange(halls);
            context.Movies.AddRange(Movies(context, halls));
            context.Snacks.AddRange(AddSnacks());

            context.SaveChanges();   
        }

        private static List<Snack> AddSnacks()
        {
            List<Snack> snacks = new List<Snack>();
            snacks.Add(new Snack
            {
                Name = "popcorn",
                imageURL = "https://static5.redcart.pl/templates/images/thumb/12319/1500/1500/pl/0/templates/images/products/12319/97fd886ca074c2e25d761c415f8753b9.jpg"
            });

            snacks.Add(new Snack
            {
                Name = "soda drink",
                imageURL = "https://img.redro.pl/plakaty/glasses-of-cola-and-orange-soda-drink-and-lemonade-400-165399885.jpg"
            });

            snacks.Add(new Snack
            {
                Name = "nachos",
                imageURL = "https://ocdn.eu/pulscms-transforms/1/IgLk9kpTURBXy9hZmI1ZTUyMTg1MmY4NDEzNmRlNTQ2MTRlMTdlNmU5Mi5qcGeTlQMAQs0En80CmZMFzQMUzQG8kwmmYmY0OTkzBoGhMAE/nachos-z-sosem-serowym.jpg"
            });

            return snacks;
        }

        private static List<Hall> AddHalls(ApplicationDbContext context)
        {
            List<Hall> halls = new List<Hall>();

            halls.Add(new Hall
            {
                HallName = "Uno",
                Rows = GeneratePrettySeats(context)
            });



            halls.Add(new Hall
            {
                HallName = "Dos",
                Rows = GeneratePrettySeats2(context)
            });

            return halls;
        }

        private static List<Movie> Movies(ApplicationDbContext context, List<Hall> halls)
        {
            List<Movie> movies = new List<Movie>();
            Random random = new Random();

            Movie shawshank = new Movie
            {
                Title = "The Shawshank Redemption",
                PosterUrl = "https://fwcdn.pl/fpo/10/48/1048/6925401.6.jpg",
                Description = "Escape from prison",
                Category = Category.Action,
                Duration = 118,
                ScreeningTimes = new List<ScreeningTime>
                { new ScreeningTime { Screening = DateTime.Now.AddDays(1), Rows = GenerateRandomSeats(12, 15), Hall = halls[random.Next(halls.Count)] },
                    new ScreeningTime { Screening = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(10), Rows = GenerateRandomSeats(10, 15), Hall = halls[random.Next(halls.Count)] }
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
                    new ScreeningTime { Screening = DateTime.Now.AddDays(2), Rows = GenerateRandomSeats(4, 5), Hall = halls[random.Next(halls.Count)] },
                    new ScreeningTime { Screening = DateTime.Now.AddDays(2).AddHours(5).AddMinutes(32), Rows = GenerateRandomSeats(5, 5), Hall = halls[random.Next(halls.Count)] }
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
                    new ScreeningTime { Screening = DateTime.Now, Rows = GeneratePrettySeats(context), Hall = halls[random.Next(halls.Count)]}
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
                    Seat seat = new Seat { Status = (SeatStatus)seatStatuses.GetValue(random.Next(seatStatuses.Length)), ColumnIndex = j };
                    seats.Add(seat);
                }

                generatedRows.Add(new Row { Seats = seats, RowIndex = i });
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

            for (int i = 0; i < seats.Length; i++)
            {
                int[] rawRow = seats[i];
                Row row = new Row { RowIndex = i };
                List<Seat> rowSeats = new List<Seat>();

                for (int j = 0; j < rawRow.Length; j++)
                {
                    int seatStatus = rawRow[j];
                    if (seatStatus == 1)
                    {
                        Seat seat = new Seat { Status = SeatStatus.Free, ColumnIndex = j };
                        rowSeats.Add(seat);
                        context.Seats.Add(seat);
                    }
                    else
                    {
                        Seat seat = new Seat { Status = SeatStatus.Excluded, ColumnIndex = j };
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

        private static List<Row> GeneratePrettySeats2(ApplicationDbContext context)
        {
            List<Row> rows = new List<Row>();
            int[][] seats = new int[][]
            {
                new int []{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                new int []{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                new int []{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                new int []{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int []{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };

            for (int i = 0; i < seats.Length; i++)
            {
                int[] rawRow = seats[i];
                Row row = new Row { RowIndex = i };
                List<Seat> rowSeats = new List<Seat>();

                for (int j = 0; j < rawRow.Length; j++)
                {
                    int seatStatus = rawRow[j];
                    if (seatStatus == 1)
                    {
                        Seat seat = new Seat { Status = SeatStatus.Free, ColumnIndex = j };
                        rowSeats.Add(seat);
                        context.Seats.Add(seat);
                    }
                    else
                    {
                        Seat seat = new Seat { Status = SeatStatus.Excluded, ColumnIndex = j };
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

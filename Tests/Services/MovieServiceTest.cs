using AutoMapper;
using CinemaApi.Data;
using CinemaApi.DTOs.Input;
using CinemaApi.Models;
using CinemaApi.Repositories;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class MovieServiceTest : IDisposable
    {
        private readonly IMovieService movieService;
        private ApplicationDbContext applicationDbContext;
        private MovieRepository movieRepository;

        public MovieServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb" + Guid.NewGuid());
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            movieRepository = new MovieRepository(applicationDbContext);
            movieService = new MovieService(movieRepository, new HallRepository(applicationDbContext));
        }

        public void Dispose()
        {
            applicationDbContext.RemoveRange(applicationDbContext.Halls);
            applicationDbContext.SaveChanges();
        }

        [Fact]
        public void ShouldCreateMovieAndCopySeats()
        {
            //Arrange
            List<Row> rows = CreateRows();
            Hall hall = CreateAndSaveHall(rows);
            CreateMovieDTO createMovieDto = CreateMovieDTODefault(hall.Id);

            //Act
            movieService.CreateMovie(createMovieDto);

            //Assert
            Movie createdMovie = movieRepository.GetByID(1);

            Assert.All(createdMovie.ScreeningTimes[0].Rows, r => rows.Any(x => x.RowIndex == r.RowIndex));
        }

        [Fact]
        public void ShouldUpdateMovie()
        {
            //Arrange
            List<Row> rows = CreateRows();
            Hall hall = CreateAndSaveHall(rows);
            CreateAndSaveHall(rows);
            CreateMovieDTO createMovieDto = CreateMovieDTODefault(hall.Id);
            movieService.CreateMovie(createMovieDto);

            UpdateMovieDTO updateMovieDTO = CreaUpdateMovieDTO();

            //Act
            movieService.UpdateMovie(updateMovieDTO);

            //Assert
            Movie updatedMovie = movieRepository.GetByID(1);

            Assert.Single(movieRepository.GetAll());
            Assert.Equal("UpdatedTitle", updatedMovie.Title);
            Assert.Equal("UpdatedPoster", updatedMovie.PosterUrl);
            Assert.Equal(100, updatedMovie.Duration);
            Assert.Equal(Category.SciFi, updatedMovie.Category);
            Assert.Equal("UpdatedDescription", updatedMovie.Description);
            Assert.Equal(1, updatedMovie.Id);
            Assert.Equal(2, updatedMovie.ScreeningTimes.Count);           
        }

        private UpdateMovieDTO CreaUpdateMovieDTO()
        {
            return new UpdateMovieDTO
            {
                Id = 1,
                Category = Category.SciFi.ToString(),
                Description = "UpdatedDescription",
                Duration = 100,
                PosterUrl = "UpdatedPoster",
                Title = "UpdatedTitle",
                ScreeningTimes = new List<CreateScreaningDTO>
                {
                    new CreateScreaningDTO
                    {
                        Date = DateTime.Now.AddDays(1),
                        HallId = 2
                    },
                    new CreateScreaningDTO
                    {
                        Date = DateTime.Now.AddDays(2),
                        HallId = 2
                    }
                }
            };
        }

        private List<Row> CreateRows()
        {
            return new List<Row>
            {
                new Row
                { RowIndex = 1, Seats = new List<Seat>
                    {
                        new Seat { ColumnIndex = 1, Status = SeatStatus.Free },
                        new Seat { ColumnIndex = 2, Status = SeatStatus.Excluded }
                    }
                },
                new Row
                {
                    RowIndex = 2, Seats = new List<Seat>
                    {
                        new Seat { ColumnIndex = 1, Status = SeatStatus.Excluded },
                        new Seat { ColumnIndex = 2, Status = SeatStatus.Free }
                    }
                }
            };
        }

        private Hall CreateAndSaveHall(List<Row> rows)
        {
            Hall hall = new Hall
            {
                Rows = rows
            };

            applicationDbContext.Halls.Add(hall);
            applicationDbContext.SaveChanges();

            return hall;
        }

        private CreateMovieDTO CreateMovieDTODefault(int hallId)
        {
            return new CreateMovieDTO
            {
                Title = "Title",
                Description = "Description",
                Duration = 1,
                PosterUrl = "PosterUrl",
                Category = Category.Action.ToString(),
                ScreeningTimes = new List<CreateScreaningDTO>
                {
                    new CreateScreaningDTO
                    {
                        Date = DateTime.Now,
                        HallId = hallId
                    }
                }
            };
        }

        private IEnumerable<Movie> GetMovies()
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

            return movies;
        }

        private List<Row> GenerateRandomSeats(int rows, int cols)
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

using AutoMapper;
using CinemaApi.DTOs.Input;
using CinemaApi.DTOs.Output;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IHallRepository hallRepository;

        public MovieService(IMovieRepository movieRepository, IHallRepository hallRepository)
        {
            this.movieRepository = movieRepository;
            this.hallRepository = hallRepository;
        }

        public bool CreateMovie(CreateMovieDTO createMovie)
        {
            Movie movie = new Movie
            {
                Title = createMovie.Title,
                Category = Enum.Parse<Category>(createMovie.Category),
                Description = createMovie.Description,
                Duration = createMovie.Duration,
                PosterUrl = createMovie.PosterUrl,
                ScreeningTimes = new List<ScreeningTime>()
            };

            foreach (var screening in createMovie.ScreeningTimes)
            {
                ScreeningTime screeningTime = new ScreeningTime
                {
                    Screening = screening.Date
                };

                Hall hall = hallRepository.GetByID(screening.HallId);

                if (hall == null)
                    return false;
                screeningTime.Hall = hall;

                CopySeatsFromHall(screeningTime, hall);
                
                movie.ScreeningTimes.Add(screeningTime);
            }

            movieRepository.Insert(movie);
            return movieRepository.SaveChanges() > 0;
        }

        private void CopySeatsFromHall(ScreeningTime screeningTime, Hall hall)
        {
            screeningTime.Rows = hall.Rows.Select(r => new Row
            {
                RowIndex = r.RowIndex,
                Seats = r.Seats
                        .Select(s => new Seat
                        {
                            ColumnIndex = s.ColumnIndex,
                            Status = s.Status
                        }).ToList()
            }).ToList();
        }

        public IEnumerable<MovieDTO> GetAllMovies()
        {
            return movieRepository.GetAll()
                .Select(x => new MovieDTO
                {
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Duration = x.Duration,
                    PosterUrl = x.PosterUrl,
                    Title = x.Title,
                    ScreeningTimes = x.ScreeningTimes
                });
        }
    }
}

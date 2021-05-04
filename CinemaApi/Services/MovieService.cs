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
                ScreeningTime screeningTime = ScreeningTimeBuilder.Init(hallRepository)
                    .SetDate(screening.Date)
                    .SetSeatsFromHall(screening.HallId)
                    .Build();
                
                movie.ScreeningTimes.Add(screeningTime);
            }

            movieRepository.Insert(movie);
            return movieRepository.SaveChanges() > 0;
        }

        public IEnumerable<MovieDTO> GetAllMovies()
        {
            return movieRepository.GetAll()
                .Select(x => new MovieDTO
                {
                    Id = x.Id,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Duration = x.Duration,
                    PosterUrl = x.PosterUrl,
                    Title = x.Title,
                    ScreeningTimes = x.ScreeningTimes
                });
        }

        public bool UpdateMovie(UpdateMovieDTO updateMovieDTO)
        {
            Movie movie = movieRepository.GetByID(updateMovieDTO.Id);
            movie.Title = updateMovieDTO.Title;
            movie.Category = Enum.Parse<Category>(updateMovieDTO.Category);
            movie.Description = updateMovieDTO.Description;
            movie.Duration = updateMovieDTO.Duration;
            movie.PosterUrl = updateMovieDTO.PosterUrl;

            movie.ScreeningTimes.Clear();
                

            foreach (var screening in updateMovieDTO.ScreeningTimes)
            {
                ScreeningTime screeningTime = ScreeningTimeBuilder.Init(hallRepository)
                    .SetDate(screening.Date)
                    .SetSeatsFromHall(screening.HallId)
                    .Build();

                movie.ScreeningTimes.Add(screeningTime);
            }

            return movieRepository.SaveChanges() > 0;
        }
    }
}

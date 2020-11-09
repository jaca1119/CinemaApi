using AutoMapper;
using CinemaApi.DTOs.Output;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public IEnumerable<MovieDTO> GetAllMovies()
        {
            return mapper.Map<List<MovieDTO>>(movieRepository.GetAll());
        }
    }
}

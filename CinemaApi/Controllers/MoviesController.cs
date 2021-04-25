using CinemaApi.DTOs.Input;
using CinemaApi.DTOs.Output;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CinemaApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public ActionResult<List<MovieDTO>> GetMovies()
        {
            return Ok(movieService.GetAllMovies());
        }

        [HttpPost]
        public ActionResult<List<MovieDTO>> CreateMovie(CreateMovieDTO createMovie)
        {
            return Created("", movieService.CreateMovie(createMovie));
        }
    }
}

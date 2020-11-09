using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<List<Movie>> GetMovies()
        {
            return Ok(movieService.GetAllMovies());
        }
    }
}

﻿using CinemaApi.Controllers.Attributes;
using CinemaApi.DTOs.Input;
using CinemaApi.DTOs.Output;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [JwtAuthorize]
        [HttpPost]
        public ActionResult<bool> CreateMovie(CreateMovieDTO createMovie)
        {
            return Created("", movieService.CreateMovie(createMovie));
        }

        [JwtAuthorize]
        [HttpPut]
        public ActionResult<bool> UpdateMovie(UpdateMovieDTO updateMovieDTO)
        {
            return Created("", movieService.UpdateMovie(updateMovieDTO));
        }
    }
}

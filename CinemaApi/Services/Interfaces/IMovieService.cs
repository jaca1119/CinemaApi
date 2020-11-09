﻿using CinemaApi.DTOs.Output;
using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<MovieDTO> GetAllMovies();
    }
}
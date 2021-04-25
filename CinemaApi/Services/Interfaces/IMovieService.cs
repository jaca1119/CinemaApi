using CinemaApi.DTOs.Output;
using System.Collections.Generic;

namespace CinemaApi.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<MovieDTO> GetAllMovies();
        bool CreateMovie(DTOs.Input.CreateMovieDTO createMovie);
    }
}

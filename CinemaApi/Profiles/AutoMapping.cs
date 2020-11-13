using AutoMapper;
using CinemaApi.DTOs.Output;
using CinemaApi.Models;

namespace CinemaApi.Profiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Movie, MovieDTO>();
        }
    }
}

using AutoMapper;
using CinemaApi.DTOs.Output;
using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Profiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<Seat, SeatDTO>();
        }
    }
}

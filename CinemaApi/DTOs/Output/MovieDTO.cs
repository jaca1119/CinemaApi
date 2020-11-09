using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Output
{
    public class MovieDTO
    {
        public string PosterUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
        public List<ScreeningTime> ScreeningTimes { get; set; }
        public List<SeatDTO> Seats { get; set; }
    }
}

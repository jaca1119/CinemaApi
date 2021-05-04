using CinemaApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Output
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
        public List<ScreeningTime> ScreeningTimes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class UpdateMovieDTO
    {
        public int Id { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Category { get; set; }
        public List<CreateScreaningDTO> ScreeningTimes { get; set; }
    }
}

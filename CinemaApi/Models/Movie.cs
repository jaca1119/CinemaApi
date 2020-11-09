using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public enum Category
    {
        Action,
        SciFi
    }
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public string PosterUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public Category Category { get; set; }
        public List<ScreeningTime> ScreeningTimes { get; set; }
        public List<Seat> Seats { get; set; }
    }
}

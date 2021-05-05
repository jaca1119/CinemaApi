using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class ScreeningTime
    {
        [Key]
        public int Id { get; set; }
        public Hall Hall { get; set; }
        public DateTimeOffset Screening { get; set; }
        public List<Row> Rows { get; set; }
    }
}
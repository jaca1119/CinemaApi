using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class ScreeningTime
    {
        [Key]
        public int ID { get; set; }
        public DateTime Screening { get; set; }
        public List<Row> Rows { get; set; }
    }
}
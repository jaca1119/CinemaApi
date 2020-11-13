using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class Row
    {
        [Key]
        public int ID { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
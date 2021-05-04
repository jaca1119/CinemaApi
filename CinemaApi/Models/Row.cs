using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class Row
    {
        [Key]
        public int Id { get; set; }
        public int RowIndex { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public enum SeatStatus
    {
        Free,
        Taken,
        Excluded
    }
    public class Seat
    {
        [Key]
        public int ID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public SeatStatus Status { get; set; }
    }
}
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
        public int Id { get; set; }
        public int ColumnIndex { get; set; }
        public SeatStatus Status { get; set; }
    }
}
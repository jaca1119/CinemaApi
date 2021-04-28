using CinemaApi.Models;

namespace CinemaApi.DTOs.Input
{
    public class SeatDTO
    {
        public int ColumnIndex { get; set; }
        public SeatStatus Status { get; set; }
    }
}
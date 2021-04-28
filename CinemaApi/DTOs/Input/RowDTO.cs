using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class RowDTO
    {
        public int RowIndex { get; set; }
        public List<SeatDTO> Seats { get; set; }
    }
}

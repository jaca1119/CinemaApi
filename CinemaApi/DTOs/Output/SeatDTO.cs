using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Output
{
    public class SeatDTO
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public SeatStatus Status { get; set; }
    }
}

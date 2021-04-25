using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class CreateScreaningDTO
    {
        public DateTime Date { get; set; }
        public int HallId { get; set; }
    }
}

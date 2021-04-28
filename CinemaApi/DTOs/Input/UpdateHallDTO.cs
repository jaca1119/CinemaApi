using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class UpdateHallDTO
    {
        public int Id { get; set; }
        public string HallName { get; set; }
        public List<RowDTO> Rows { get; set; }
    }
}

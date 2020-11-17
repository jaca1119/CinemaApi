using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class TicketDTO
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int[] SelectedSeats { get; set; }
    }
}

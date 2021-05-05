using CinemaApi.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.DTOs.Input
{
    public class OrderDTO
    {
        public int MovieId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int[] SelectedSeats { get; set; }
        public List<SnackDTO> Snacks { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}

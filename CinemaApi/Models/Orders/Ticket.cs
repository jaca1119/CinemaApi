using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models.Orders
{
    public enum TicketType
    {
        Normal,
        Student
    }
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public TicketType TicketType { get; set; }
        public int Quantity { get; set; }
    }
}

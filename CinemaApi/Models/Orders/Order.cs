using CinemaApi.Models.Snacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models.Orders
{
    public enum PaymentStatus
    {
        Pending,
        Accepted
    }
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Movie Movie { get; internal set; }
        public List<Seat> Seats { get; internal set; }
        public DateTime? OrderDate { get; internal set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public List<SnackOrder> Snacks { get; internal set; }
        public ScreeningTime Screening { get; internal set; }
        public List<Ticket> Tickets { get; set; }
    }
}

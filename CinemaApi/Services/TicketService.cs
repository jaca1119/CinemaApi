using CinemaApi.DTOs.Input;
using CinemaApi.Models;
using CinemaApi.Models.Orders;
using CinemaApi.Models.Snacks;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly ISeatRepository seatRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ISnackRepository snackRepository;

        public TicketService(ISeatRepository seatRepository, IMovieRepository movieRepository, IOrderRepository orderRepository, ISnackRepository snackRepository)
        {
            this.seatRepository = seatRepository;
            this.movieRepository = movieRepository;
            this.orderRepository = orderRepository;
            this.snackRepository = snackRepository;
        }

        public int? AcceptTicket(OrderDTO orderDTO)
        {
            Movie movie = movieRepository.GetByID(orderDTO.MovieId);
            if (movie == null)
                return null;


            List<Seat> seats = new List<Seat>();
            foreach (var seatId in orderDTO.SelectedSeats)
            {
                Seat seat = seatRepository.GetByID(seatId);

                if (seat == null)
                    return null;

                seat.Status = SeatStatus.Taken;
                seats.Add(seat);
            }

            List<SnackOrder> snackOrders = new List<SnackOrder>();
            foreach (var snack in orderDTO.Snacks)
            {
                Snack orderedSnack = snackRepository.GetByID(snack.Id);
                if (orderedSnack == null)
                    return null;

                snackOrders.Add(new SnackOrder
                {
                    Snack = orderedSnack,
                    Quantity = snack.Quantity,
                    Size = snack.Size
                });
            }

            Order order = new Order
            {
                Movie = movie,
                Seats = seats,
                OrderDate = DateTime.UtcNow,
                Snacks = snackOrders,
                Screening = movie.ScreeningTimes.FirstOrDefault(s => s.Screening.ToUnixTimeSeconds() == orderDTO.Date.ToUnixTimeSeconds()),
                Tickets = orderDTO.Tickets
            };

            orderRepository.Insert(order);
            orderRepository.SaveChanges();

            return order.Id;
        }
    }
}

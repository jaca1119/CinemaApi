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

        public TicketService(ISeatRepository seatRepository)
        {
            this.seatRepository = seatRepository;
        }
        public bool AcceptTicket(DTOs.Input.TicketDTO ticket)
        {
            foreach (var seatId in ticket.SelectedSeats)
            {
                Models.Seat seat = seatRepository.GetByID(seatId);

                if (seat == null)
                {
                    return false;
                }

                seat.Status = Models.SeatStatus.Taken;
                seatRepository.SaveChanges();
            }

            return true;
        }
    }
}

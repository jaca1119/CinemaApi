using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services.Interfaces
{
    public interface ITicketService
    {
        bool AcceptTicket(DTOs.Input.TicketDTO ticket);
    }
}

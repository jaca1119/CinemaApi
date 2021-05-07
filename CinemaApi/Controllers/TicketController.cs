using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.DTOs.Input;
using CinemaApi.Models.Orders;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        public ActionResult<int?> ProcessTicket(OrderDTO orderDTO)
        {
            return Created("", ticketService.AcceptTicket(orderDTO));
        }

    }
}

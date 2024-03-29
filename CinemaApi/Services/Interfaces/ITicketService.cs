﻿using CinemaApi.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services.Interfaces
{
    public interface ITicketService
    {
        int? AcceptTicket(DTOs.Input.OrderDTO orderDTO);
    }
}

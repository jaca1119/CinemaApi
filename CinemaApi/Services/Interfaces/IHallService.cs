using CinemaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services.Interfaces
{
    public interface IHallService
    {
        IEnumerable<Hall> GetAll();
        bool CreateHall(Hall hall);
        bool UpdateHall(Hall hall);
    }
}

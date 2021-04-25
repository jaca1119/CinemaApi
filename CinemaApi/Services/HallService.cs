using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository hallRepository;

        public HallService(IHallRepository hallRepository)
        {
            this.hallRepository = hallRepository;
        }

        public bool CreateHall(Hall hall)
        {
            hallRepository.Insert(hall);
            return hallRepository.SaveChanges() > 0;
        }

        public IEnumerable<Hall> GetAll()
        {
            return hallRepository.GetAll();
        }

        public bool UpdateHall(Hall updateHall)
        {
            Hall hall = hallRepository.GetByID(updateHall.Id);

            if (hall == null)
                return false;

            hall.HallName = updateHall.HallName;
            hall.Rows = updateHall.Rows;

            return hallRepository.SaveChanges() > 0;
        }
    }
}

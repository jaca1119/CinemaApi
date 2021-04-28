using CinemaApi.DTOs.Input;
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

        public bool UpdateHall(UpdateHallDTO updateHall)
        {
            Hall hall = hallRepository.GetByID(updateHall.Id);

            if (hall == null)
                return false;

            hall.HallName = updateHall.HallName;
            hall.Rows = updateHall.Rows.Select(r => new Row { RowIndex = r.RowIndex, Seats = r.Seats.Select(s => new Seat { ColumnIndex = s.ColumnIndex, Status = s.Status}).ToList()}).ToList();

            return hallRepository.SaveChanges() > 0;
        }
    }
}

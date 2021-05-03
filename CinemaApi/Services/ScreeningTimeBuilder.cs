using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class ScreeningTimeBuilder
    {
        private ScreeningTime screeningTime = new ScreeningTime();
        private readonly IHallRepository hallRepository;

        public ScreeningTimeBuilder(IHallRepository hallRepository)
        {
            this.hallRepository = hallRepository;
        }

        public static ScreeningTimeBuilder Init(IHallRepository hallRepository)
        {
            return new ScreeningTimeBuilder(hallRepository);
        }

        public ScreeningTimeBuilder SetDate(DateTime date)
        {
            screeningTime.Screening = date;
            return this;
        }

        public ScreeningTimeBuilder SetSeatsFromHall(int hallId)
        {
            Hall hall = hallRepository.GetByID(hallId);
            screeningTime.Hall = hall;

            CopySeatsFromHall(screeningTime, hall);
            return this;
        }

        public ScreeningTime Build()
        {
            return screeningTime;
        }

        private void CopySeatsFromHall(ScreeningTime screeningTime, Hall hall)
        {
            screeningTime.Rows = hall.Rows.Select(r => new Row
            {
                RowIndex = r.RowIndex,
                Seats = r.Seats
                        .Select(s => new Seat
                        {
                            ColumnIndex = s.ColumnIndex,
                            Status = s.Status
                        }).ToList()
            }).ToList();
        }
    }
}

using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Repositories
{
    public class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        public SeatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

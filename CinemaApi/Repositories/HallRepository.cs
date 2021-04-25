using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Repositories
{
    public class HallRepository : BaseRepository<Hall>, IHallRepository
    {
        public HallRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Hall> GetAll()
        {
            return dbSet.Include(x => x.Rows).ThenInclude(h => h.Seats);
        }

        public override Hall GetByID(object id)
        {
            return dbSet.Include(h => h.Rows).ThenInclude(r => r.Seats).FirstOrDefault(h => h.Id == (int)id);
        }
    }
}

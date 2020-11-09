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
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Movie> GetAll()
        {
            return dbSet.Include(e => e.ScreeningTimes).Include(e => e.Seats).ToList();
        }
    }
}

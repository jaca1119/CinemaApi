using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CinemaApi.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<Movie> GetAll()
        {
            return dbSet
                .Include(e => e.ScreeningTimes).ThenInclude(s => s.Rows).ThenInclude(r => r.Seats)
                .Include(e => e.ScreeningTimes).ThenInclude(s => s.Hall)
                .ToList();
        }
    }
}

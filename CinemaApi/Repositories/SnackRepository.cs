using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Repositories
{
    public class SnackRepository : BaseRepository<Snack>, ISnackRepository
    {
        public SnackRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

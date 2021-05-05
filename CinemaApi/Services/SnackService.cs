using CinemaApi.Models.Snacks;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class SnackService : ISnackService
    {
        private readonly ISnackRepository snackRepository;

        public SnackService(ISnackRepository snackRepository)
        {
            this.snackRepository = snackRepository;
        }
        public List<Snack> GetAllSnacks()
        {
            return snackRepository.GetAll().ToList();
        }
    }
}

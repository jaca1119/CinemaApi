using CinemaApi.Models.Snacks;
using System.Collections.Generic;

namespace CinemaApi.Services.Interfaces
{
    public interface ISnackService
    {
        List<Snack> GetAllSnacks();
    }
}
using CinemaApi.Models.Snacks;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Controllers
{
    [Route("api/snacks")]
    [ApiController]
    public class SnacksController : ControllerBase
    {
        private readonly ISnackService snackService;

        public SnacksController(ISnackService snackService)
        {
            this.snackService = snackService;
        }

        [HttpGet]
        public ActionResult<List<Snack>> GetAllSnacks()
        {
            return Ok(snackService.GetAllSnacks());
        }
    }
}

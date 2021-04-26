using CinemaApi.Controllers.Attributes;
using CinemaApi.Models;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Controllers
{
    [Route("api/halls")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private IHallService hallService;
        public HallController(IHallService hallService)
        {
            this.hallService = hallService;
        }

        [HttpGet]
        public ActionResult<List<Hall>> GetHalls()
        {
            return Ok(hallService.GetAll());
        }

        [JwtAuthorize]
        [HttpPost]
        public ActionResult<List<Hall>> CreateHall(Hall hall)
        {
            return Created("", hallService.CreateHall(hall));
        }

        [JwtAuthorize]
        [HttpPut]
        public ActionResult<List<Hall>> UpdateHall(Hall hall)
        {
            return Ok(hallService.UpdateHall(hall));
        }
    }
}

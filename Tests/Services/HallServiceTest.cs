using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class HallServiceTest : IDisposable
    {
        private IHallRepository hallRepository;
        private IHallService hallService;
        private ApplicationDbContext applicationDbContext;

        public HallServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb" + Guid.NewGuid());
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            hallRepository = new HallRepository(applicationDbContext);
            hallService = new HallService(hallRepository);
        }

        public void Dispose()
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Dispose();
        }

        [Fact]
        public void ShouldCreateAndSaveHallWithSeatsInCorrectOrder()
        {
            //Arrange
            Hall hall = CreateHall();

            //Act
            hallService.CreateHall(hall);

            //Assert
            IEnumerable<Hall> halls = hallService.GetAll();
            List<Seat> seats = halls.First().Rows.First().Seats.ToList();

            Assert.NotEmpty(halls);
            Assert.Single(halls);
            Assert.Equal("asd", halls.First().HallName);
            Assert.Equal(SeatStatus.Free, seats[0].Status);
            Assert.Equal(SeatStatus.Excluded, seats[1].Status);
            Assert.Equal(SeatStatus.Free, seats[2].Status);
        }

        [Fact]
        public void ShouldCreateAndGetHall()
        {
            //Arrange
            Hall hall = new Hall
            {
                HallName = "asd"
            };
            //Act
            hallService.CreateHall(hall);

            //Assert
            IEnumerable<Hall> halls = hallService.GetAll();

            Assert.NotEmpty(halls);
            Assert.Single(halls);
            Assert.Equal("asd", halls.First().HallName);
            Assert.Empty(halls.First().Rows);
        }

        [Fact]
        public void ShouldUpdateHall()
        {
            //Arrange
            Hall hall = CreateHall();
            hallService.CreateHall(hall);

            string updatedHallName = "UpdatedName";
            Hall updateHall = new Hall { Id = hall.Id, HallName = updatedHallName, Rows = new List<Row> { new Row { RowIndex = 1, Seats = new List<Seat> { new Seat { ColumnIndex = 1, Status = SeatStatus.Excluded } } } } };

            //Act
            hallService.UpdateHall(updateHall);

            //Assert
            Assert.Equal(updatedHallName, hall.HallName);
            Assert.Single(hall.Rows);
            Assert.Single(hall.Rows[0].Seats);
        }

        private Hall CreateHall()
        {
            return new Hall
            {
                Id = 1,
                HallName = "asd",
                Rows = new List<Row>
                {
                    new Row
                    {
                        Seats = new List<Seat>
                        {
                            new Seat
                            {
                                Status = SeatStatus.Free
                            },
                            new Seat
                            {
                                Status = SeatStatus.Excluded
                            },
                            new Seat
                            {
                                Status = SeatStatus.Free
                            }
                        }
                    }
                }
            };
        }
    }
}

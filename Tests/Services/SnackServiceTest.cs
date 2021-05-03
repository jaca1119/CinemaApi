using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class SnackServiceTest : IDisposable
    {
        private ISnackRepository snackRepository;
        private ApplicationDbContext applicationDbContext;
        private ISnackService snackService;

        public SnackServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb" + Guid.NewGuid());
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            snackRepository = new SnackRepository(applicationDbContext);
            snackService = new SnackService(snackRepository);
        }

        public void Dispose()
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Dispose();
        }

        [Fact]
        public void ShouldGetAllSnacks()
        {
            //Arrange
            Snack snack = new Snack { imageURL = "testUrl", Name = "testName" };
            applicationDbContext.Snacks.Add(snack);
            applicationDbContext.SaveChanges();

            //Act
            List<Snack> snacks = snackService.GetAllSnacks();

            //Assert
            Assert.Single(snacks);
            Assert.Equal(snack.imageURL, snacks[0].imageURL);
            Assert.Equal(snack.Name, snacks[0].Name);
        }
    }
}

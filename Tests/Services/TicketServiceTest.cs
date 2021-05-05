using CinemaApi.Data;
using CinemaApi.DTOs.Input;
using CinemaApi.Models;
using CinemaApi.Models.Orders;
using CinemaApi.Models.Snacks;
using CinemaApi.Repositories;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Services
{

    public class TicketServiceTest: IDisposable
    {
        static DateTimeOffset movieScreeningTime = DateTimeOffset.Now;

        ITicketService ticketService;

        ISeatRepository seatRepository;
        IOrderRepository orderRepository;
        IMovieRepository movieRepository;
        ISnackRepository snackRepository;

        ApplicationDbContext applicationDbContext;
        ScreeningTime screeningTime = new ScreeningTime { Screening = movieScreeningTime };

    public TicketServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb" + Guid.NewGuid());
            applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            seatRepository = new SeatRepository(applicationDbContext);
            orderRepository = new OrderRepository(applicationDbContext);
            movieRepository = new MovieRepository(applicationDbContext);
            snackRepository = new SnackRepository(applicationDbContext);

            seatRepository.Insert(new Seat { Status = SeatStatus.Free });
            seatRepository.Insert(new Seat { Status = SeatStatus.Free });
            movieRepository.Insert(new Movie { Title = "Test", ScreeningTimes = new List<ScreeningTime> { screeningTime } });
            snackRepository.Insert(new Snack { Name = "SnackTest1" });
            snackRepository.Insert(new Snack { Name = "SnackTest2" });

            applicationDbContext.SaveChanges();

            ticketService = new TicketService(seatRepository, movieRepository, orderRepository, snackRepository);
        }

        public void Dispose()
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Dispose();
        }

        [Fact]
        public void ShouldAcceptTicketAndChangeSeatStatus()
        {
            //Arange
            OrderDTO orderDTO = new OrderDTO
            {
                MovieId = 1,
                Date = screeningTime.Screening,
                SelectedSeats = new int[] { 1, 2 },
                Snacks = new List<SnackDTO> { new SnackDTO { Id = 1, Quantity = 2, Size = Size.Medium } }
            };


            //Act
            bool isTicketAccepted = ticketService.AcceptTicket(orderDTO);

            //Assert
            Assert.True(isTicketAccepted);
            Assert.Equal(SeatStatus.Taken, seatRepository.GetByID(1).Status);
            Assert.Equal(SeatStatus.Taken, seatRepository.GetByID(2).Status);
        }

        [Fact]
        public void ShouldNotAcceptTicketWhenSeatNotExists()
        {
            //Arange
            OrderDTO orderDTO = new OrderDTO
            {
                MovieId = 1,
                Date = screeningTime.Screening,
                SelectedSeats = new int[] { 1, 2, 8 }
            };

            //Act
            bool isTicketAccepted = ticketService.AcceptTicket(orderDTO);

            //Assert
            Assert.False(isTicketAccepted);
        }

        [Fact]
        public void ShouldAcceptTicketWithSnacksAndCreateOrder()
        {
            //Arange
            OrderDTO orderDTO = new OrderDTO
            {
                MovieId = 1,
                Date = new DateTimeOffset(movieScreeningTime.DateTime),
                SelectedSeats = new int[] { 1, 2 },
                Snacks = new List<SnackDTO> { new SnackDTO { Id = 1, Quantity = 2, Size = Size.Medium } },
                Tickets = new List<Ticket> { new Ticket { TicketType = TicketType.Normal, Quantity = 3} }
            };

            //Act
            bool isTicketAccepted = ticketService.AcceptTicket(orderDTO);

            //Assert
            Order order = orderRepository.GetByID(1);

            Assert.True(isTicketAccepted);

            Assert.NotNull(order);
            Assert.NotNull(order.Movie);
            Assert.Equal(order.Seats[0], seatRepository.GetAll().First(s => order.Seats[0].Id == s.Id));
            Assert.Equal(order.Seats[1], seatRepository.GetAll().First(s => order.Seats[1].Id == s.Id));
            Assert.Equal(PaymentStatus.Pending, order.PaymentStatus);

            Assert.NotNull(order.Screening);
            Assert.Equal(movieScreeningTime.Millisecond, order.Screening.Screening.Millisecond);

            Assert.NotEmpty(order.Snacks);
            Assert.Equal(orderDTO.Snacks[0].Quantity, order.Snacks[0].Quantity);
            Assert.Equal(orderDTO.Snacks[0].Size, order.Snacks[0].Size);
            Assert.NotEmpty(order.Tickets);
            Assert.True(order.OrderDate.HasValue);
        }
    }
}

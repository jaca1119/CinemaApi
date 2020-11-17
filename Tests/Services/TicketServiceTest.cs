using CinemaApi.DTOs.Input;
using CinemaApi.Models;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Services
{
    
    public class TicketServiceTest
    {
        Mock<ISeatRepository> seatRepository;

        public TicketServiceTest()
        {
            seatRepository = new Mock<ISeatRepository>();
            seatRepository.Setup(m => m.GetByID(1)).Returns(new Seat() { ID = 1, Status = SeatStatus.Free });
            seatRepository.Setup(m => m.GetByID(2)).Returns(new Seat() { ID = 2, Status = SeatStatus.Free });
        }
        [Fact]
        public void ShouldAcceptTicketAndChangeSeatStatus()
        {
            //Arange
            ITicketService ticketService = GetTicketService();

            TicketDTO ticket = new TicketDTO
            {
                Title = "Title",
                Date = DateTime.Now,
                SelectedSeats = new int[] { 1, 2 }
            };
            //Act
            bool isTicketAccepted = ticketService.AcceptTicket(ticket);

            //Assert
            Assert.True(isTicketAccepted);
            Assert.Equal(SeatStatus.Taken, seatRepository.Object.GetByID(1).Status);
            Assert.Equal(SeatStatus.Taken, seatRepository.Object.GetByID(2).Status);
        }

        private TicketService GetTicketService()
        {
            return new TicketService(seatRepository.Object);
        }
    }
}

using CinemaApi;
using CinemaApi.DTOs.Input;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Tests.IntegrationTests
{
    public class TicketControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public TicketControllerTest(WebApplicationFactory<Startup> web)
        {
            Client = web.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(p => new Mock<ITicketService>().Object);
                });
            })
                .CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async void AcceptTicket()
        {
            //Arrange
            OrderDTO orderDTO = new OrderDTO() { MovieId = 1 };
            string json = JsonConvert.SerializeObject(orderDTO);

            //Act
            var response = await Client.PostAsync("api/ticket", new StringContent(json, Encoding.UTF8, "application/json"));
            
            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}

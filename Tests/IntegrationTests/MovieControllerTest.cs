using CinemaApi;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Tests.IntegrationTests
{
    public class MovieControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public MovieControllerTest(WebApplicationFactory<Startup> web)
        {
            Client = web.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(p => new Mock<IMovieService>().Object);
                });
            })
                .CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async void ShouldReturnAllMovies()
        {
            //Arrange
            string moviesURL = "api/movies";

            //Act
            var response = await Client.GetAsync(moviesURL);

            //Assert
            string content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(content);
        }
    }
}

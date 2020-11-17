using CinemaApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureClient(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:5001");

            base.ConfigureClient(client);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models
{
    public class AuthenticateResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}

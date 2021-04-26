using CinemaApi.Models;
using CinemaApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApi.Services
{
    public class UserService : IUserService
    {
        private readonly string secret;

        //TODO: Hash password
        private List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin123"}
        };

        public UserService(IOptions<JwtSettings> options)
        {
            this.secret = options.Value.Secret;
        }

        public AuthenticateResponse Authenticate(AuthenticationRequest authenticationRequest)
        {
            User user = users.FirstOrDefault(u => u.Username == authenticationRequest.Username && u.Password == authenticationRequest.Password);
            if (user == null)
                return null;

            string token = GenerateJwtToken(user);

            return new AuthenticateResponse { Username = user.Username, Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}

using CinemaApi.Models;
using CinemaApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApi.Util
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string secret;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> options)
        {
            this.next = next;
            this.secret = options.Value.Secret;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && token != string.Empty)
                AttachUserToContext(context, token);

            await next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(secret);
                jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.Items["Authorized"] = "authorized";
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

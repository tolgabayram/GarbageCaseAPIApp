using GarbageCaseAPI.Authentication.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GarbageCaseAPI.Authentication.Implemantation
{
    public class Auth : IJwtAuth
    {
        private readonly string username1 = "tolga";
        private readonly string password1 = "bayram";
        private readonly string key;
        public Auth(string key)
        {
            this.key = key;
        }
        public string Authentication(string username, string password)
        {
            var storedUserName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceAuthentication")["UserName"];
            var storedPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ServiceAuthentication")["Password"];

            // null
            if (!(username.Equals(storedUserName) || password.Equals(storedPassword)))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = System.Text.Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}

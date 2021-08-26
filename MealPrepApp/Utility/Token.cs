using MealPrepApp.Data.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepApp.Utility
{
    public  class Token : IToken
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public (string, string) GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("email", user.Email),
                new Claim("phoneNumber", user.PhoneNumber),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("role", "Admin")

            });

           

            var secretKey = _configuration.GetSection("Authentication: JwtBearer: SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var audience = _configuration.GetSection("Authentication: JwtBearer: Audience").Value;
            var issuer = _configuration.GetSection("Authentication: JwtBearer: Issuer").Value;

            var expirationTimeInString = _configuration.GetSection("Authentication: JwtBearer: Expiration").Value;
            var expirationTimeInInteger = Convert.ToInt32(expirationTimeInString);
            var expirationTimeSpan = TimeSpan.FromMinutes(expirationTimeInInteger);

            var signInCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expirationTimeSpan),
                signingCredentials: signInCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var expirationTimeSpanInString = expirationTimeSpan.ToString();

            return (token, expirationTimeSpanInString);
        }
    }
}

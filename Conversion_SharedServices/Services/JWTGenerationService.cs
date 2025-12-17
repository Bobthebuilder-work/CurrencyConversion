using Conversion.Domain.Models.Shared;
using Conversion_SharedServices.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Conversion_SharedServices.Services
{
    public class JWTGenerationService : IJWTGenerationService
    {
        private readonly IConfiguration _config;

        public JWTGenerationService(IConfiguration config)
        {
            _config = config;
        }
        public TokenResponse GenerateToken(string email,string? roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
            };

            if (roles.Count() <= 0)
            {
                roles = "user" ;
            }

            claims.Add(new Claim(ClaimTypes.Role, roles));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.UtcNow.AddMinutes(
                int.Parse(_config["Jwt:ExpiryMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new TokenResponse
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                expiresAt = expiry
            };
        }
    }
}

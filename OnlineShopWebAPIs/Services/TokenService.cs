using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domains.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShopWebAPIs.Models;

namespace BusinessLogic.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(IdentityUserContext applicationUser)
        {
            var signingCredentials = GetSigningCredentials();
            var claims =  GetClaims(applicationUser);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["Jwt:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(IdentityUserContext user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.firstName)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(

                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(Convert.ToDouble((_configuration.GetSection("Jwt")).GetSection("Lifetime").Value))
                );

            return tokenOptions;
        }


    }
}
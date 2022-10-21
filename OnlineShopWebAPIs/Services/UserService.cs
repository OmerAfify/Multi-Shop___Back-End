using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Interfaces.IServices;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.SettingsModels;

namespace OnlineShopWebAPIs.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUserContext> _userManager { get; }

        private IOptions<TokenSettings> _tokenSettings;

        private IdentityUserContext _user;
        

        public UserService(UserManager<IdentityUserContext> userManager, IOptions<TokenSettings> tokenSettings)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettings;
        }

        public async Task<bool> ValidateUser (LoginUserDTO loginUserDTO)
        {
            _user = await _userManager.FindByNameAsync(loginUserDTO.email);

            if (_user != null && await _userManager.CheckPasswordAsync(_user, loginUserDTO.password))
                return true;
            else
                return false;
        }
        public async Task<string> CreateToken(){
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

     
        

        

        
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(

                issuer: _tokenSettings.Value.ValidIssuer,
                audience: _tokenSettings.Value.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_tokenSettings.Value.lifetime)),
                signingCredentials: signingCredentials

                );


            return tokenOptions;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _tokenSettings.Value.Key;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        }
        

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;

        }
        
    }
}

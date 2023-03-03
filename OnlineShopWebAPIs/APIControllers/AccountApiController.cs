using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Models;

namespace CoffeeCorner.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUserContext> _userManager;
        private readonly SignInManager<IdentityUserContext> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserController(UserManager<IdentityUserContext> userManager,
            SignInManager<IdentityUserContext> signInManager,
            ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {


            try
            {    
                var email =  User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return BadRequest();

                return Ok(new 
                {
                    email = user.Email,
                    name = user.firstName,
                    token = _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return Problem("an error occured.");
            }

        }


        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        //{


        //    try
        //    {  //extrntion method being used 
        //        var user = await _userManager.GetUserAddressWithClaimsPrincipal(User);


        //        if (user == null)
        //            return BadRequest();

        //        return Ok(_mapper.Map<AddressDTO>(user.address));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem("an error occured.");
        //    }
        //}

        //[HttpPut]
        //[Authorize]
        //public async Task<ActionResult<AddressDTO>> UpdateUserAddress([FromBody] AddressDTO addressDTO)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());


        //    try
        //    {
        //        var user = await _userManager.GetUserAddressWithClaimsPrincipal(User);

        //        user.address = _mapper.Map<Address>(addressDTO);

        //        var result = await _userManager.UpdateAsync(user);

        //        if (!result.Succeeded)
        //            return BadRequest();

        //        return Ok(_mapper.Map<AddressDTO>(user.address));

        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(" error has occured. " + ex.Message);

        //    }

        //}


        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserDTO registerUserDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                if (CheckIfEmailExistsAsync(registerUserDTO.email).Result.Value)
                    return BadRequest(new { Errors = new List<string> { "Email already exists." } });


                var user = new IdentityUserContext()
                {

                    Email = registerUserDTO.email,
                    UserName = registerUserDTO.email,
                    firstName = registerUserDTO.firstName,
                    lastName = registerUserDTO.lastName
                };

                var result = await _userManager.CreateAsync(user, registerUserDTO.password);

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        Email = user.Email,
                        Name = user.firstName,
                        Token = _tokenService.CreateToken(user)
                    });

                }
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error : "+ ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginUserDTO loginUserDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDTO.email);

                if (user == null)
                    return BadRequest(new { Errors = new List<string> { "email doesn't exists." } });

                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.email, loginUserDTO.password, false, false);

                if (!result.Succeeded)
                    return BadRequest(new { Errors = new List<string> { "Password doesn't match this email." } });


                return Ok(new
                {
                    Email = user.Email,
                    Name = user.firstName,
                    Token = _tokenService.CreateToken(user)
                });


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error : " + ex.Message);
            }

        }


        [HttpGet]
        public async Task<ActionResult<bool>> CheckIfEmailExistsAsync([FromQuery] string email)
        {
            try
            {

                return await _userManager.FindByEmailAsync(email) != null;

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error : " + ex.Message);
            }


        }



    }
}
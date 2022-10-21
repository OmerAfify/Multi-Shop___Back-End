using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Interfaces.IServices;
using OnlineShopWebAPIs.Models;

namespace OnlineShopWebAPIs.APIControllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private UserManager<IdentityUserContext> _userManager { get; }
        private IUserService _userService{ get; }
        private IMapper _mapper { get; }
        private ILogger<AccountApiController> _logger { get; }

        public AccountApiController(UserManager<IdentityUserContext> userManager, 
            IUserService userService,  IMapper mapper,ILogger<AccountApiController> logger)
        {     
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;

        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserDTO userDTO )
        {
            try {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _mapper.Map<IdentityUserContext>(userDTO);

                user.UserName = userDTO.email;

                var result = await _userManager.CreateAsync(user, userDTO.password);

                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, "Customer");

                return Accepted();

            
            }catch(Exception ex)
            {
                _logger.LogError("Sonething whent wrong in " + nameof(Register), ex);
                return StatusCode(500, "Server Error : " + ex.Message);

            }
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUserDTO loginUserDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

              if(! await _userService.ValidateUser(loginUserDTO))
                {
                    return Unauthorized();
                }
                else
                {
                    return Accepted(new { Token = await _userService.CreateToken() });
                }


            }
            catch (Exception ex)
            {
                _logger.LogError("Sonething whent wrong in " + nameof(Login), ex);
                return StatusCode(500, "Server Error : " + ex.Message);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.IBusinessRepository;
using Models.Models;
using OnlineShopWebAPIs.DTOs;

namespace OnlineShopWebAPIs.APIControllers
{
   
    [ApiController]
    [Route("api/[action]")]

    public class ShoppingCartController : ControllerBase
    {

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;


        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCartAsync(string id)
        {
            try {
               return _mapper.Map<ShoppingCartDTO>(await _shoppingCartRepository.GetShoppingCartByIdAsync(id));
            
            } 
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }



        [HttpPost]
        public async Task<ActionResult<ShoppingCartDTO>> CreateOrUpdateShoppingCartAsync(ShoppingCartDTO shoppingCartDTO)
        {
            try
            {

              var cart = await _shoppingCartRepository.CreateOrUpdateShoppingCartAsync(_mapper.Map<ShoppingCart>(shoppingCartDTO));

                return _mapper.Map<ShoppingCartDTO>(cart);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShoppingCartAsync(string id)
        {
            try
            {
                return Ok(await _shoppingCartRepository.DeleteShoppingCartAsync(id));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}

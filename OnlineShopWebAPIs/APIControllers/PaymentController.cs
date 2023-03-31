using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.IServices;
using Models.Models;

namespace OnlineShopWebAPIs.APIControllers
{
    [Route("api/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
     
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string shoppingCartId)
        {
            try {

                return Ok(await _paymentService.CreateOrUpdatePaymentIntent(shoppingCartId));
            
            
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }





    }
}

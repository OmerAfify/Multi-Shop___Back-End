using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.IServices;
using Models.Models;

namespace APIControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[action]")]



    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

        }


        [HttpPost]
        public async Task<ActionResult<OrderReturnedDTO>> CreateOrder(OrderDTO orderDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                //ModelState is Valid

                var user = User.FindFirstValue(ClaimTypes.Email);
                var address = _mapper.Map<OrderAddress>(orderDTO.shippingAddress);


          
                var order = await _orderService.CreateOrder(user, orderDTO.shoppingCartId,
                                                            orderDTO.DeliveryMethodId, address);

                if (order != null)
                    return Ok(_mapper.Map<OrderReturnedDTO>(order));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReturnedDTO>>> GetUserOrders()
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var orders = await _orderService.GetUserOrdersAsync(user);

                if (orders != null)
                    return Ok(_mapper.Map<List<OrderReturnedDTO>>(orders));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<OrderReturnedDTO>> GetUserOrderById(int orderId)
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var order = await _orderService.GetOrderByIdAsync(user, orderId);

                if (order != null)
                    return Ok(_mapper.Map<OrderReturnedDTO>(order));
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDeliveryMethods>>> GetDeliveryMethods()
        {
            try
            {

                var deliveryMethods = await _orderService.GetDeliveryMethods();

                if (deliveryMethods != null)
                    return Ok(deliveryMethods);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }









    }
}

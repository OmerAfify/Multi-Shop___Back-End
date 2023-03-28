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
        private IMapper _mapper { get; }

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;

        }


        [HttpPost]
        public async Task<ActionResult<OrderReturnedDTO>> CreateOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);
                var address = _mapper.Map<OrderAddress>(orderDTO.shippingAddress);

                var order =  _orderService.CreateOrder(user, orderDTO.shoppingCart,
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
        public ActionResult<OrderReturnedDTO> GetUserOrders()
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var orders = _orderService.GetUserOrders(user);

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
        public ActionResult<OrderReturnedDTO> GetUserOrderById(int orderId)
        {
            try
            {
                var user = User.FindFirstValue(ClaimTypes.Email);

                var order = _orderService.GetOrderById(user, orderId);

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
        public ActionResult<IEnumerable<OrderDeliveryMethods>> GetDeliveryMethods()
        {
            try
            {

                var deliveryMethods = _orderService.GetDeliveryMethods();

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

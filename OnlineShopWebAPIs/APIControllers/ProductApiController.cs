using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Interfaces.IUnitOfWork;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.DBContext;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductApiController : Controller
    {

        private IUnitOfWork _unitOfWork{ get;}
        private IMapper _mapper{ get;}
        private ILogger<ProductApiController> _logger{ get;}


        public ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductApiController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try {
                
                _logger.LogInformation("api '/api/GetAllProducts' is being accessed by user _x_ .");
                return Ok(_mapper.Map<List<ProductDTO>>(_unitOfWork.Products.GetAll(new List<string>() { "category" })));
            
            }catch(Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetAllProducts));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

          
        }

 
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                _logger.LogInformation($"api  '/api/GetProductById/{id}' is being accessed by user _x_ .");
                return Ok(_mapper.Map<ProductDTO>(_unitOfWork.Products.Find(c => c.productId == id, new List<string>() { "category" })));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetProductById));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

            
        }

    

    }
}

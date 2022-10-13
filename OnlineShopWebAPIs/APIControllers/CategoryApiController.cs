using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Interfaces.IUnitOfWork;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.DBContext;

namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CategoryApiController : Controller
    {

        private IUnitOfWork _unitOfWork{ get;}
        private IMapper _mapper{ get;}
        private ILogger<CategoryApiController> _logger{ get;}


        public CategoryApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryApiController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try {
                
                _logger.LogInformation("api '/api/GetAllCategories' is being accessed by user _x_ .");
                return Ok(_mapper.Map<List<CategoryDTO>>(_unitOfWork.Categories.GetAll(new List<string>() { "products" })));
            
            }catch(Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetAllCategories));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

          
        }


        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                _logger.LogInformation($"api  '/api/GetCategoryById/{id}' is being accessed by user _x_ .");
                return Ok(_mapper.Map<CategoryDTO>(_unitOfWork.Categories.Find(c => c.categoryId == id, new List<string>() { "products" })));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetCategoryById));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

            
        }

    

    }
}

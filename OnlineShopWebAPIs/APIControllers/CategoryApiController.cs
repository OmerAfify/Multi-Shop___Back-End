using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using OnlineShopWebAPIs.DTOs;
namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CategoryApiController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryApiController> _logger;


        public CategoryApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryApiController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            try {
                var categories = await _unitOfWork.Categories.GetAllAsync(new List<string>() { "products" });

                if (categories == null)
                    return NotFound();

                return Ok( _mapper.Map<List<CategoryDTO>>(categories));
            
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
                var category = _unitOfWork.Categories.FindAsync(c => c.categoryId == id, new List<string>() { "products" });

                if (category == null)
                    return NotFound();

                return Ok(_mapper.Map<CategoryDTO>(category));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetCategoryById));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

            
        }



        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromForm] AddCategoryDTO addCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
            
                Category category = _mapper.Map<Category>(addCategoryDTO);

                _unitOfWork.Categories.InsertAsync(category);
                var result = await _unitOfWork.Save();


                if (result > 0)
                    return Accepted(category);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(AddNewCategory));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }
        }




    }
}

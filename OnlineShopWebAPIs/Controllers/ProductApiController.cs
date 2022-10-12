using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Interfaces.IUnitOfWork;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.DBContext;

namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductApiController : Controller
    {

        private IUnitOfWork _unitOfWork{ get;}
        private IMapper _mapper{ get;}


        public ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, OnlineShopDbContext onlineShopDb)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<List<ProductDTO>>(_unitOfWork.Products.GetAll(new List<string>(){"category"}) );
        }


        [HttpGet]
        public ProductDTO GetProductById(int id)
        {
            return _mapper.Map<ProductDTO>(_unitOfWork.Products.Find(c => c.productId == id, new List<string>() {"category"}) );
        }

    

    }
}

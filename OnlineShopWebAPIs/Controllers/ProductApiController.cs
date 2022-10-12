using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPIs.Interfaces.IUnitOfWork;
using OnlineShopWebAPIs.Models;

namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductApiController : Controller
    {

        private IUnitOfWork _unitOfWork{ get;}

        public ProductApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Products.GetAll();

        }


        [HttpGet]
        public Product GetProductById(int id)
        {
            return _unitOfWork.Products.GetById(id);

        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Helpers;
using OnlineShopWebAPIs.Interfaces.IUnitOfWork;
using OnlineShopWebAPIs.Models;
using OnlineShopWebAPIs.Models.SettingsModels;
using OnlineShopWebAPIs.ViewModels;


namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductApiController : Controller
    {

        private IUnitOfWork _unitOfWork{ get;}
        private IMapper _mapper{ get;}
        private ILogger<ProductApiController> _logger{ get;}
        private IOptions<WebAppSettings> _webAppSettings { get; }

        public ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductApiController> logger, IOptions<WebAppSettings> webAppSettings )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _webAppSettings = webAppSettings;

        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try {
                
                _logger.LogInformation("api '/api/GetAllProducts' is being accessed by user _x_ .");

                var productsList = _mapper.Map<List<ProductDTO>>(_unitOfWork.Products.GetAll(new List<string>() { "category", "productImages" }));   
                   productsList.ForEach(c => c.productImages.ForEach(i=>i.productImagePath = _webAppSettings.Value.HostName +  i.productImagePath));
                
                return Ok(productsList);
            
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
               
                var product = _mapper.Map<ProductDTO>(_unitOfWork.Products.Find(c => c.productId == id, new List<string>() { "category", "productImages" }));
                product.productImages.ForEach(i=>i.productImagePath = _webAppSettings.Value.HostName + i.productImagePath);


                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetProductById));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

        }


        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromForm]ProductInFormVm productInFormVm) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation($"api  '/api/AddNewProduct/' is being accessed by user _x_ .");

                Product product = _mapper.Map<Product>(productInFormVm.addProductDTO);
                _unitOfWork.Products.Insert(product);
                _unitOfWork.Save();

                if (productInFormVm.images != null )
                {
                  
                    foreach(var image in productInFormVm.images)
                    {
                        //Upload file + get the image's created unique Name.
                        var imageName = await ImageUploadingHelper.UploadImage(image, Path.Combine(Directory.GetCurrentDirectory(), @"wwwRoot\Images\ProductsImages\cat" + product.categoryId));

                        //Get the uploaded images full path
                        var imagesPath = Path.Combine(@"Images/ProductsImages/cat" + product.categoryId+ "/"+ imageName);

                        //new product Image Object
                        ProductImage productImage = new ProductImage() {
                            productId = product.productId,
                            productImageName = imageName,
                            productImagePath = imagesPath
                        };

                        _unitOfWork.ProductImages.Insert(productImage);
                        _unitOfWork.Save();
                    }
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(AddNewProduct));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }
        }

    
        [HttpPut]
        public async Task<IActionResult> UpdateProduct (int id, [FromForm]ProductInFormVm productInFormVm)
        {

            if (!ModelState.IsValid || id < 1 )
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation($"api  '/api/UpdateProduct/' is being accessed by user _x_ .");


                var product = _unitOfWork.Products.GetById(id);
               
                if(product == null)
                {
                    return BadRequest("the submitted data is invalid");
                }

                _mapper.Map(productInFormVm.addProductDTO, product);

                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();


                if (productInFormVm.images != null)
                {

                    foreach (var image in productInFormVm.images)
                    {
                        //if(image.FileName is included in path /Cat+ catID )
                        //{
                        //    continue;

                        //// TODO list
                        // 1- get list of images names
                        // 2- if exists in DB dont raise it again
                        // 3 - if doesnt exists in db . raise it.
                        // 4- else remove all rest in the db


                        //}


                        //upload file + get the image's created unique name
                        var imagename = await ImageUploadingHelper.UploadImage(image, Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId));

                        //get the uploaded images full path
                        var imagespath = Path.Combine(@"Images/Productsimages/cat" + product.categoryId + "/" + imagename);

                        //new product image object
                        ProductImage productImage = new ProductImage()
                        {
                            productId = product.productId,
                            productImageName = imagename,
                            productImagePath = imagespath
                        };

                        _unitOfWork.ProductImages.Insert(productImage);
                        _unitOfWork.Save();
                    }
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(UpdateProduct));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            if (id < 1)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation($"api  '/api/DeleteProduct/' is being accessed by user _x_ .");


                var product = _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    return BadRequest("the submitted data is invalid");
                }


                var ImagesList = _unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList();

                foreach(var image in ImagesList)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName );
                    ImageUploadingHelper.DeleteImage(path);
                }

                _unitOfWork.Products.Delete(product);

                _unitOfWork.Save();

                return NoContent();  
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(DeleteProduct));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

        }




    }
}

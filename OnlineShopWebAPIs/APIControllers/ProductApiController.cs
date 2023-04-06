using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Filtration;
using Models.Interfaces.IUnitOfWork;
using Models.Models;
using Models.ViewModels;
using OnlineShopWebAPIs.DTOs;
using OnlineShopWebAPIs.Helpers;
using OnlineShopWebAPIs.Models.SettingsModels;


namespace OnlineShopWebAPIs.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ProductApiController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductApiController> _logger;
        private readonly IOptions<WebAppSettings> _webAppSettings;

        public ProductApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductApiController> logger, IOptions<WebAppSettings> webAppSettings )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _webAppSettings = webAppSettings;

        }


      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try {
                
             
                var productsList = _mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.GetAllAsync(new List<string>() { "category", "productImages" }));   
    
                return Ok(productsList);
            
            }catch(Exception ex)
            {
                 return StatusCode(500, ex.Message + "Internal Server error. Please try again later.");

            }

          
        }

 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            try
            {
                
                var product = _mapper.Map<ProductDTO>(await _unitOfWork.Products.FindAsync(c => c.productId == id, new List<string>() { "category", "productImages" }));
      
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetProductById));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }

        }


        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetRelatedProductsByCategory(int productId)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(productId);


                var relatedProductsList = _mapper.Map<List<ProductDTO>>(await _unitOfWork.Products.FindRangeAsync(p => p.productId != productId && p.categoryId == product.categoryId,
                                                              new List<string>() { "category", "productImages" }));

                return Ok(relatedProductsList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Something went wrong in " + nameof(GetAllProducts));
                return StatusCode(500, "Internal Server error. Please try again later.");

            }


        }



        [HttpGet]
        public async Task<IActionResult> GetProductsByFiltration([FromQuery] FilteringObject filteringObject)
        {
            try
            {
                var pagination = await _unitOfWork.Products.GetProductsByFilteration(filteringObject);
                
                return Ok(_mapper.Map<ProductPaginationDTO>(pagination));

            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }






        // [HttpPost]
        // public async Task<IActionResult> AddNewProduct([FromForm]ProductInFormVm productInFormVm) 
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     try
        //     {
        //         _logger.LogInformation($"api  '/api/AddNewProduct/' is being accessed by user _x_ .");

        //         Product product = _mapper.Map<Product>(productInFormVm.addProductDTO);
        //         _unitOfWork.Products.Insert(product);
        //         _unitOfWork.Save();

        //         if (productInFormVm.images != null )
        //         {

        //             foreach(var image in productInFormVm.images)
        //             {
        //                 //Upload file + get the image's created unique Name.
        //                 var imageName = await ImageUploadingHelper.UploadImage(image, Path.Combine(Directory.GetCurrentDirectory(), @"wwwRoot\Images\ProductsImages\cat" + product.categoryId));

        //                 //Get the uploaded images full path
        //                 var imagesPath = Path.Combine(@"Images/ProductsImages/cat" + product.categoryId+ "/"+ imageName);

        //                 //new product Image Object
        //                 ProductImage productImage = new ProductImage() {
        //                     productId = product.productId,
        //                     productImageName = imageName,
        //                     productImagePath = imagesPath
        //                 };

        //                 _unitOfWork.ProductImages.Insert(productImage);
        //                 _unitOfWork.Save();
        //             }
        //         }

        //         return Accepted();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, " Something went wrong in " + nameof(AddNewProduct));
        //         return StatusCode(500, "Internal Server error. Please try again later.");

        //     }
        // }


        // [HttpPut]
        //// [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> UpdateProduct (int id, [FromForm]ProductInFormVm productInFormVm)
        // {

        //     if (!ModelState.IsValid || id < 1 )
        //         return BadRequest(ModelState);

        //     try
        //     {
        //         _logger.LogInformation($"api  '/api/UpdateProduct/' is being accessed by user _x_ .");

        //         var product = _unitOfWork.Products.GetById(id);

        //         if(product == null)
        //             return BadRequest("the submitted data is invalid");

        //         var ImagesList = _unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList();

        //         foreach (var image in ImagesList)
        //             ImageUploadingHelper.DeleteImage(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName));

        //         _unitOfWork.ProductImages.DeleteRange(ImagesList);


        //         if (productInFormVm.images != null)
        //         {
        //             foreach (var image in productInFormVm.images)
        //             {

        //               var imagename = await ImageUploadingHelper.UploadImage(image, Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + productInFormVm.addProductDTO.categoryId));
        //               var imagespath = Path.Combine(@"Images/Productsimages/cat" + productInFormVm.addProductDTO.categoryId + "/" + imagename);

        //                ProductImage productImage = new ProductImage()
        //                  {
        //                       productId = product.productId,
        //                       productImageName = imagename,
        //                       productImagePath = imagespath
        //                  };

        //              _unitOfWork.ProductImages.Insert(productImage);

        //             }

        //         }


        //         _mapper.Map(productInFormVm.addProductDTO, product);
        //         _unitOfWork.Products.Update(product);


        //         _unitOfWork.Save();

        //         return NoContent();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, " Something went wrong in " + nameof(UpdateProduct));
        //         return StatusCode(500, "Internal Server error. Please try again later.");

        //     }

        // }


        // [HttpDelete]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> DeleteProduct(int id)
        // {

        //     if (id < 1)
        //         return BadRequest(ModelState);

        //     try
        //     {
        //         _logger.LogInformation($"api  '/api/DeleteProduct/' is being accessed by user _x_ .");


        //         var product = _unitOfWork.Products.GetById(id);

        //         if (product == null)
        //         {
        //             return BadRequest("the submitted data is invalid");
        //         }


        //         var ImagesList = _unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList();

        //         foreach(var image in ImagesList)
        //         {
        //             var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName );
        //             ImageUploadingHelper.DeleteImage(path);
        //         }

        //         _unitOfWork.Products.Delete(product);

        //         _unitOfWork.Save();

        //         return NoContent();  

        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, " Something went wrong in " + nameof(DeleteProduct));
        //         return StatusCode(500, "Internal Server error. Please try again later.");

        //     }

        // }






        //OLD UPDATE CODE>>>>

        //[HttpPut]
        //public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductInFormVm productInFormVm)
        //{

        //    if (!ModelState.IsValid || id < 1)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        _logger.LogInformation($"api  '/api/UpdateProduct/' is being accessed by user _x_ .");

        //        var product = _unitOfWork.Products.GetById(id);

        //        if (product == null)
        //            return BadRequest("the submitted data is invalid");


        //        var ImagesList = _unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList();

        //        if (productInFormVm.images != null)
        //        {

        //            var removedImagesNames = ImagesList.Select(i => i.productImageName).Except(productInFormVm.images.Select(i => i.FileName));
        //            var removedImagesColumns = _unitOfWork.ProductImages.FindRange(i => removedImagesNames.Contains(i.productImageName));


        //            foreach (var image in removedImagesColumns)
        //                ImageUploadingHelper.DeleteImage(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName));

        //            _unitOfWork.ProductImages.DeleteRange(removedImagesColumns.ToList());
        //            _unitOfWork.Save();


        //            foreach (var image in productInFormVm.images)
        //            {

        //                var Img = _unitOfWork.ProductImages.Find(i => i.productId == product.productId && i.productImageName == image.FileName);

        //                if (Img == null)
        //                {
        //                    var imagename = await ImageUploadingHelper.UploadImage(image, Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId));
        //                    var imagespath = Path.Combine(@"Images/Productsimages/cat" + product.categoryId + "/" + imagename);

        //                    ProductImage productImage = new ProductImage()
        //                    {
        //                        productId = product.productId,
        //                        productImageName = imagename,
        //                        productImagePath = imagespath
        //                    };

        //                    _unitOfWork.ProductImages.Insert(productImage);
        //                    _unitOfWork.Save();
        //                }


        //            }

        //            if (productInFormVm.addProductDTO.categoryId != product.categoryId)
        //            {
        //                var NewImgList = _unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList();


        //                foreach (var image in NewImgList)
        //                {

        //                    var source = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName);
        //                    var dest = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + productInFormVm.addProductDTO.categoryId + @"\" + image.productImageName);

        //                    var result = ImageUploadingHelper.MoveImage(source, dest);

        //                    if (result)
        //                        image.productImagePath = Path.Combine(@"Images/Productsimages/cat" + productInFormVm.addProductDTO.categoryId + "/" + image.productImageName);

        //                }

        //            }
        //        }
        //        else
        //        {

        //            foreach (var image in ImagesList)
        //                ImageUploadingHelper.DeleteImage(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\Productsimages\cat" + product.categoryId + @"\" + image.productImageName));

        //            _unitOfWork.ProductImages.DeleteRange(_unitOfWork.ProductImages.FindRange(i => i.productId == product.productId).ToList());
        //        }


        //        _mapper.Map(productInFormVm.addProductDTO, product);
        //        _unitOfWork.Products.Update(product);


        //        _unitOfWork.Save();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, " Something went wrong in " + nameof(UpdateProduct));
        //        return StatusCode(500, "Internal Server error. Please try again later.");

        //    }

        //}




    }
}

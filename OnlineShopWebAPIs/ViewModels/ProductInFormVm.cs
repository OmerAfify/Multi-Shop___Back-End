using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineShopWebAPIs.DTOs;

namespace Models.ViewModels
{
    public class ProductInFormVm
    {
        public List<IFormFile> images { get; set; }
        public AddProductDTO addProductDTO { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class ProductDTO
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string ImageName { get; set; }
        public double salesPrice { get; set; }
        public string description { get; set; }
        // public int categoryId { get; set; }
         public string categoryName { get; set; }


    }
}

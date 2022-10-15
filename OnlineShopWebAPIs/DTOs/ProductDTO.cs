using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class AddProductDTO
      {      
        public string productName { get; set; }
        public double salesPrice { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }


  
      }
    
    
    public class ProductDTO : AddProductDTO
    {
      public int productId { get; set; }
      public string categoryName { get; set; }
      public List<ProductImageDTO> productImages { get; set; }

    }

 
}

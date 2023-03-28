using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double salesPrice { get; set; }
        public string description { get; set; }

        public List<Review> reviews { get; set; }
        public List<ProductImage> productImages { get; set; }

        public int categoryId { get; set; }
        public Category category { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ProductImage
    {
        public int productImageId { get; set; }
        public string productImageName { get; set; }
        public string productImagePath { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
    }
}

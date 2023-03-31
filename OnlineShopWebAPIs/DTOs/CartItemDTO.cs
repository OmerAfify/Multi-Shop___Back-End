using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class CartItemDTO
    {
        public int productId { get; set; }

        public string productName { get; set; }
        public string categoryName { get; set; }
        public string productImage { get; set; }

        [Required]
        [Range(0.1, 10000, ErrorMessage = "Price should be greater than 0 and atmost 10000")]
        public double salesPrice { get; set; }


        [Required]
        [Range(1, 100, ErrorMessage = "Quantity must be atleast 1 and atmost 100.")]
        public int quantity { get; set; }

    }
}

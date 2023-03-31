using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ShoppingCart
    {

        public string id { get; set; }
        public List<CartItem> items { get; set; }

        //stripe properties
        public int? DeliveryMethodId{ get; set; }
        public string ClientSecret{ get; set; }
        public string PaymentIntentId{ get; set; }
        public decimal shippingPrice{ get; set; }

    }
    
    public class CartItem
    {
        public int productId { get; set; }

        public string ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }


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

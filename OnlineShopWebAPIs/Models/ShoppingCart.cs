using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.Models
{
    public class ShoppingCart
    {

        public string id { get; set; }
        public List<CartItem> items { get; set; }
     
    }
    
    public class CartItem
    {
            public int productId { get; set; }
            public string productName { get; set; }
            public string categoryName { get; set; }
            public string productImage { get; set; }
            public decimal salesPrice { get; set; }
            public int quantity { get; set; }
    }
}

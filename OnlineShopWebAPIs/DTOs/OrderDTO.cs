using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebAPIs.Models;

namespace DTOs
{
    public class OrderDTO
    {
    
        public ShoppingCart shoppingCart { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressDTO shippingAddress { get; set; }
    }
}

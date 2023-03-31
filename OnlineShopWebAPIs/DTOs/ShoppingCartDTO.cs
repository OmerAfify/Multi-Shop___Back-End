using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class ShoppingCartDTO
    {

        public string id { get; set; }
        public List<CartItemDTO> items { get; set; }

        //stripe properties
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal shippingPrice { get; set; }

    }

   
}

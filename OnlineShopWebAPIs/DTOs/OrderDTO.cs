
using Models.Models;

namespace DTOs
{
    public class OrderDTO
    {
    
        public ShoppingCart shoppingCart { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressDTO shippingAddress { get; set; }
    }
}

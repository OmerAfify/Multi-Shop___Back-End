
using Models.Models;

namespace DTOs
{
    public class OrderDTO
    {
    
        public string shoppingCartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressDTO shippingAddress { get; set; }
    }
}

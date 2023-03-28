using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class OrderedItem
    {
        public OrderedItem()
        {

        }

        public OrderedItem(ProductItemOrdered productItemOrdered, int quantity, decimal totalPrice)
        {

            ProductItemOrdered = productItemOrdered;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public int Id { get; set; }
        public ProductItemOrdered ProductItemOrdered{ get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {

        }
        public ProductItemOrdered(int productId, string productName, string pictureUrl, decimal salesPrice)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            SalesPrice = salesPrice;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal SalesPrice { get; set; }

    }
}

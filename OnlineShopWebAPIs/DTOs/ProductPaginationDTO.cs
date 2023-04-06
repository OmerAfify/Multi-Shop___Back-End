using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.RequestParameters;

namespace OnlineShopWebAPIs.DTOs
{
    public class ProductPaginationDTO
    {
        public int count { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public IEnumerable<ProductDTO> data { get; set; }

    }
}

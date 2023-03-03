using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{


    public class AddCategoryDTO
    {
        [Required]
        public string categoryName { get; set; }
        public string categoryDescription { get; set; }
     }

    public class CategoryDTO : AddCategoryDTO
    {
        public int categoryId { get; set; }
          public int productsNumber { get; set; }
    }
}

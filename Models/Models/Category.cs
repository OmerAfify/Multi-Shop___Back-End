﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebAPIs.Models;

namespace Models.Models
{
    public class Category
    {
        public int categoryId { get; set; }
       
       [Required]
        public string categoryName { get; set; }

        public string categoryImagePath { get; set; }

        public string categoryDescription { get; set; }

        public List<Product> products { get; set; }
    }
}

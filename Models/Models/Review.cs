using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Review
    {

        public int reviewId { get; set; }

        // public int customerId { get; set; }
        // public User user { get; set; }

        public string reviewDescription { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pagination
{
    public class Pagination<T> where T : class
    {
        public int count { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }

        public IEnumerable<T> data { get; set; }

    }
}

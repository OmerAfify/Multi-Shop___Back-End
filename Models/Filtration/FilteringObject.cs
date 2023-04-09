using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RequestParameters;

namespace Models.Filtration
{
    public class FilteringObject
    {
        public int categoryId { get; set; }
        public string sortBy { get; set; }

        public string search { get { return _search; }
            set { _search = (value != null) ? value.ToLower() : null; }
                             }
        private string _search;
        public RequestParams requestParameters{ get; set; }
    }
}

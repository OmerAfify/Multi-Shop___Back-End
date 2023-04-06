using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestParameters
{
    public class RequestParams
    {
        const int MaxPageSize = 50;

        private int _pageSize = 20;

        private int _pageNumber = 1;

        public int PageNumber 
        { 
            get { return _pageNumber; } 
            set { _pageNumber = (value <= 0 ) ?  1 : value; }
        } 


        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize :(value<=0) ? 1 : value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class LoginUserDTO  
    { 
        public string email { get; set; }
        public string password { get; set; }
   
    }
    
    
    public class UserDTO : LoginUserDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
   
    }    
    
   



   
}

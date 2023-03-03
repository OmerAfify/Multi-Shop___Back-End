using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebAPIs.DTOs
{
    public class LoginUserDTO  
    { 
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must have Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]

        public string password { get; set; }
   
    }
    
    
    public class UserDTO : LoginUserDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
   
    }    
    
   



   
}

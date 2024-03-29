﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
    public class IdentityUserContext : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public  Address address { get; set; }
    }
}

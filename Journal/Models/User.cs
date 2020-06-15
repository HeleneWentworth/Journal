using System;
using Microsoft.AspNetCore.Identity;

namespace Journal.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
    }
}

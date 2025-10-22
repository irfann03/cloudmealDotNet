using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class UserDTO
    {
        public string? Email { get; set; }
    
        public string? Password { get; set; }
        public UserType UserType { get; set; }

        public CustomerDTO? Customer { get; set; }

        public KitchenDTO? Kitchen { get; set; }
    }
}
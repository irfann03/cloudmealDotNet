using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public UserType UserType { get; set; }
    }
}
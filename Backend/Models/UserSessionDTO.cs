using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserSessionDTO
    {
         public string? Token { get; set; }
    public string? UserType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    }
}
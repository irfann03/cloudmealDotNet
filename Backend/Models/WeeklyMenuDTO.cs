using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class WeeklyMenuDTO
    {
        public int MenuId { get; set; }
        public Day DayOfWeek { get; set; }
    }
}
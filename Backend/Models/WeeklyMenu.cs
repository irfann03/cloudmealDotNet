using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class WeeklyMenu
    {
        [Key]
        public int WeeklyMenuId { get; set; }

        public DateTime StartOfWeek { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
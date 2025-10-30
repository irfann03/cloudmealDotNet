using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{

    public enum Day { MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY }
    public class WeeklyMenu
    {
        [Key]
        public int WeeklyMenuId { get; set; }

        public Day DayOfWeek { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }

        [ForeignKey("Kitchen")]
        public int KitchenId { get; set; }
        public Kitchen? Kitchen { get; set; }

        public WeeklyMenu() { }

        public WeeklyMenu(Day dayOfWeek, int customerId, int menuId, int kitchenId)
        {
            DayOfWeek = dayOfWeek;
            CustomerId = customerId;
            MenuId = menuId;
            KitchenId = kitchenId;
        }
    }
}
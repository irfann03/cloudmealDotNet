using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum MenuType { VEG, NON_VEG }

public class Menu
{
    [Key]
    public int MenuId { get; set; }

    public string MenuName { get; set; }
    public int Price { get; set; }
    public MenuType MenuType { get; set; }

    public List<MenuItems> MenuItems { get; set; } = new();
    public List<Orders> Orders { get; set; } = new();
    public List<FeedBack> Feedbacks { get; set; } = new();
    public List<WeeklyMenu> WeeklyMenu { get; set; } = new();

    [ForeignKey("Kitchen")]
    public int KitchenId { get; set; }
    public Kitchen Kitchen { get; set; }
}
}
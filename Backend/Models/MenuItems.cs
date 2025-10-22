using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum MenuItemType { TIFFIN, LUNCH, DINNER }
    public class MenuItems
{
    [Key]
    public int MenuItemId { get; set; }

    public string Name { get; set; }
    public int Price { get; set; }

    public MenuItemType ItemType { get; set; }
    public MenuType MenuType { get; set; }

    public List<Menu> Menus { get; set; } = new();

    [ForeignKey("Kitchen")]
    public int? KitchenId { get; set; }
    public Kitchen Kitchen { get; set; }
}
}
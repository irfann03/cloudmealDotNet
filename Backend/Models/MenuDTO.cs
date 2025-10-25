using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class MenuDTO
    {
        public int menuId { get; set; }
        public string MenuName { get; set; } = null!;
        public int Price { get; set; }
        public MenuType MenuType { get; set; }

        public List<int> MenuItemIds { get; set; } = new();

        public MenuDTO() { }

        public MenuDTO(string menuName, int price, MenuType menuType, List<int> menuItemIds)
        {
            MenuName = menuName;
            Price = price;
            MenuType = menuType;
            MenuItemIds = menuItemIds;
        }

        public MenuDTO(int MenuId, string menuName, int price, MenuType menuType)
        {
            menuId = MenuId;
            MenuName = menuName;
            Price = price;
            MenuType = menuType;
        }
    }
}
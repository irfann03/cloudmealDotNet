using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Backend.Models
{
    public class MenuResponseDTO
    {
        public int menuId { get; set; }
        public string MenuName { get; set; } = null!;
        public int Price { get; set; }
        public MenuType MenuType { get; set; }

        public MenuResponseDTO() { }

        public MenuResponseDTO(int Id,string menuName, int price, MenuType menuType)
        {
            menuId = Id;
            MenuName = menuName;
            Price = price;
            MenuType = menuType;
        }
    }
}
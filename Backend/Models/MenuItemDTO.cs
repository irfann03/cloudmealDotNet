using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class MenuItemDTO
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public MenuItemType ItemType { get; set; }
        public MenuType MenuType { get; set; }

    }
}
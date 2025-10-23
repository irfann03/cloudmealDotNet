using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class MenuDTO
    {
        public string MenuName { get; set; } = null!;
        public int Price { get; set; }
        public MenuType MenuType { get; set; }
        public List<int> MenuItemIds { get; set; } = new();
    }
}
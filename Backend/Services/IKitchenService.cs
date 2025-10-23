using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface IKitchenService
    {
        public Task<MenuItemDTO> AddMenuItemAsync(MenuItemDTO menuItemDto, string token);
        public Task<MenuDTO> CreateMenuAsync(MenuDTO menuDto, string token);
    }
}
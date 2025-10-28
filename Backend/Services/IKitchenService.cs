using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IKitchenService
    {
        public Task<MenuItemDTO> AddMenuItemAsync(MenuItemDTO menuItemDto, string token);
        public Task<MenuDTO> CreateMenuAsync(MenuDTO menuDto, string token);
        public Task<IEnumerable<MenuItemDTO>> GetMenuItemsByTypeAsync(string type, string token);
        public Task<IEnumerable<MenuItemDTO>> GetMenuItemsAsync(string token);
        public Task<IEnumerable<MenuResponseDTO>> GetMenusAsync(string token);
        public Task<IEnumerable<MenuItemDTO>> GetMenuItemsByMenuIdAsync(int menuId, string token);
    }
}
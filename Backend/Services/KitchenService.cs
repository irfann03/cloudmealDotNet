using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Configurations;
using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Services
{
    public class KitchenService : IKitchenService
    {

        private readonly MyDBContext _dbContext;
        private readonly IAuthService _authService;

        public KitchenService(MyDBContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<MenuItemDTO> AddMenuItemAsync(MenuItemDTO menuItemDto, string token)
        {

            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
                throw new UnauthorizedAccessException("session expired, Please login first");

            if (session.UserType != UserType.KITCHEN)
                throw new UnauthorizedAccessException("Invalid session token for kitchen");

            var kitchen = await _dbContext.Kitchen.FirstOrDefaultAsync(k => k.UserId == session.UserId);
            if (kitchen == null)
                throw new Exception("Kitchen not found for this user");

            var menuItem = new MenuItems
            {
                Name = menuItemDto.Name,
                Price = menuItemDto.Price,
                ItemType = menuItemDto.ItemType,
                MenuType = menuItemDto.MenuType,
                KitchenId = kitchen.KitchenId,
                Kitchen = kitchen
            };

            _dbContext.MenuItems.Add(menuItem);
            await _dbContext.SaveChangesAsync();

            return new MenuItemDTO
            {
                Name = menuItem.Name,
                Price = menuItem.Price,
                ItemType = menuItem.ItemType,
                MenuType = menuItem.MenuType,
            };
        }

        public async Task<MenuDTO> CreateMenuAsync(MenuDTO menuDto, string token)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
                throw new UnauthorizedAccessException("session expired, Please login first");

            if (session.UserType != UserType.KITCHEN)
                throw new UnauthorizedAccessException("Invalid session token for kitchen");

            var kitchen = await _dbContext.Kitchen
            .Include(kitchen => kitchen.MenuItems)
            .FirstOrDefaultAsync(k => k.UserId == session.UserId);

            if (kitchen == null)
                throw new Exception("Kitchen not found for this user");

            var selectedMenuItems = kitchen.MenuItems
                .Where(mi => menuDto.MenuItemIds.Contains(mi.MenuItemId)).ToList();

            if (selectedMenuItems.Count != menuDto.MenuItemIds.Count)
            {
                throw new Exception("One or more MenuItems not found in kitchen's menu items");
            }

            var menu = new Menu
            {
                MenuName = menuDto.MenuName,
                Price = menuDto.Price,
                MenuType = menuDto.MenuType,
                Kitchen = kitchen,
                MenuItems = selectedMenuItems
            };

            _dbContext.Menu.Add(menu);
            await _dbContext.SaveChangesAsync();

            return menuDto;
        }
    }
}
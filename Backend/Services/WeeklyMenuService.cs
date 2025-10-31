using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Configurations;


namespace Backend.Services
{

    public class WeeklyMenuService : IWeeklyMenuService
    {
        private readonly MyDBContext _dbContext;
        private readonly IAuthService _authService;

        public WeeklyMenuService(MyDBContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<WeeklyMenuDTO> createWeeklyMenuAsync(WeeklyMenuDTO weeklyMenuDTO, String token)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
            {
                throw new UnauthorizedAccessException("Invalid session token.");
            }

            var customer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.UserId == session.UserId);

            if (customer == null)
            {
                throw new ArgumentException("Customer not found for the given user.");
            }

            var menu = await _dbContext.Menu
                .Include(m => m.Kitchen)
                 .FirstOrDefaultAsync(m => m.MenuId == weeklyMenuDTO.MenuId);

            if (menu == null)
            {
                throw new ArgumentException("Menu not found for the given MenuId.");
            }

            var kitchen = menu.Kitchen;

            var WeeklyMenu = new WeeklyMenu(weeklyMenuDTO.DayOfWeek, customer.CustomerId, weeklyMenuDTO.MenuId, kitchen!.KitchenId);

            _dbContext.WeeklyMenu.Add(WeeklyMenu);
            _dbContext.SaveChanges();

            return weeklyMenuDTO;
        }

        public async Task<IEnumerable<DailyOrderResponseDTO>> getTodayOrderedMenuAsync(String token)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
            {
                throw new UnauthorizedAccessException("Invalid session token.");
            }

            var kitchen = await _dbContext.Kitchen.FirstOrDefaultAsync(k => k.UserId == session.UserId);
            if (kitchen == null)
            {
                throw new ArgumentException("Kitchen not found for the given user.");
            }

            var today = (Day)Enum.Parse(typeof(Day), DateTime.Now.DayOfWeek.ToString().ToUpper());
            var todayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), today.ToString(), true);


            Console.WriteLine(today);

            var orderedMenus = await _dbContext.WeeklyMenu
    .Where(wm => wm.DayOfWeek == today && wm.KitchenId == kitchen.KitchenId)
    .Where(wm => !_dbContext.Orders.Any(o => o.CustomerId == wm.CustomerId && o.MenuId == wm.MenuId &&
        o.KitchenId == wm.KitchenId && o.DeliveryDate.DayOfWeek == todayOfWeek))
    .Select(wm => new DailyOrderResponseDTO(wm.CustomerId, wm.MenuId)).ToListAsync();
            return orderedMenus;
        }
    }
}
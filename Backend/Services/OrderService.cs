using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Configurations;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly MyDBContext _dbContext;
        private readonly IAuthService _authService;

        public OrderService(MyDBContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<String> completeOrder(String token, int customerId, int menuId)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
            {
                throw new UnauthorizedAccessException("invalid or expired session token");
            }

            var kitchen = await _dbContext.Kitchen
            .Include(k => k.Menus)
            .FirstOrDefaultAsync(k => k.UserId == session.UserId);

            if (kitchen == null)
            {
                throw new ArgumentException("kitchen not found");
            }

            var menu = kitchen.Menus.FirstOrDefault(m => m.MenuId == menuId);

            if(menu == null)
            {
                 throw new ArgumentException("menu not found");

            }

            var customer = await _dbContext.Customer
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new ArgumentException($"no customer found for id : {customerId}");
            }

            var today = (Day)Enum.Parse(typeof(Day), DateTime.Now.DayOfWeek.ToString().ToUpper());


            var weeklyMenuOrder = await _dbContext.WeeklyMenu
            .FirstOrDefaultAsync(wm => wm.CustomerId == customer.CustomerId &&
                                wm.KitchenId == kitchen.KitchenId &&
                                wm.MenuId == menu.MenuId &&
                                wm.DayOfWeek == today
            );

            if(weeklyMenuOrder == null)
            {
                return null!;
            }

            var order = new Orders
            {
                Customer = customer,
                Kitchen = kitchen,
                Menu = menu,
                TotalAmount = menu.Price
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return "Order Completed";
        }
    }
}
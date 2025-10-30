using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IWeeklyMenuService
    {
        public Task<WeeklyMenuDTO> createWeeklyMenu(WeeklyMenuDTO weeklyMenuDTO, String token);
        public Task<List<int>> getTodayOrderedMenu(String token);
    }
}
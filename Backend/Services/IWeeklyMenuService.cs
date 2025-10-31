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
        public Task<WeeklyMenuDTO> createWeeklyMenuAsync(WeeklyMenuDTO weeklyMenuDTO, String token);
        public Task<IEnumerable<DailyOrderResponseDTO>> getTodayOrderedMenuAsync(String token);
    }
}
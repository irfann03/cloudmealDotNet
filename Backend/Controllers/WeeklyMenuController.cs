using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeeklyMenuController : ControllerBase
    {
        private readonly IWeeklyMenuService _weeklyMenuService;
        public WeeklyMenuController(IWeeklyMenuService weeklyMenuService)
        {
            _weeklyMenuService = weeklyMenuService;
        }

        [HttpPost]
        public async Task<IActionResult> createWeeklyMenu([FromBody] WeeklyMenuDTO weeklyMenuDTO, [FromHeader] String token)
        {
            try
            {
                var result = await _weeklyMenuService.createWeeklyMenu(weeklyMenuDTO, token);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("todayOrderedMenu")]
        public Task<List<int>> getTodayOrderedMenu([FromHeader] String token)
        {
            try
            {
                var result = _weeklyMenuService.getTodayOrderedMenu(token);
                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
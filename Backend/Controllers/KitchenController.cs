using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitchenController : ControllerBase
    {
        private readonly IKitchenService _kitchenService;
        public KitchenController(IKitchenService kitchenService)
        {
            _kitchenService = kitchenService;
        }

        [HttpPost("menuItem")]
        public async Task<IActionResult> AddMenuItem([FromBody] MenuItemDTO menuItemDto, [FromHeader] string token)
        {
            try
            {
                var result = await _kitchenService.AddMenuItemAsync(menuItemDto, token);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("menu")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuDTO menuDto, [FromHeader] string token)
        {
            try
            {
                var result = await _kitchenService.CreateMenuAsync(menuDto, token);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
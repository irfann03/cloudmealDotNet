using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Address")]
        public async Task<IActionResult> AddAddress([FromBody] Address address, [FromHeader] String token)
        {
            Console.WriteLine("In Add Address Controller");
            var result = await _customerService.AddAddressAsync(address, token);
            if (result == "Please login first." || result == "Invalid session token.")
                return Unauthorized(result);
            return Ok(result);
        }

        [HttpPost("{menuId}/Feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] String comment, [FromHeader] String token, [FromRoute(Name = "menuId")] int id)
        {
            try
            {
                var result = await _customerService.SubmitFeedbackAsync(comment, id, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("rechargeHistory")]
        public async Task<ActionResult<IEnumerable<RechargeHistory>>> GetRechargeHistory([FromHeader] String token)
        {
            try
            {
                var result = await _customerService.GetRechargeHistoryAsync(token);
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

        [HttpGet("kitchens")]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetKitchens([FromHeader] String token)
        {
            try
            {
                var result = await _customerService.GetKitchensAsync(token);
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

        [HttpGet("{kitchenId}/menus")]
        public async Task<ActionResult<IEnumerable<MenuItemDTO>>> GetMenusForKitchenId([FromHeader] String token, [FromRoute] int kitchenId)
        {
            try
            {
                var result = await _customerService.GetMenusByKitchenIdAsync(kitchenId, token);
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
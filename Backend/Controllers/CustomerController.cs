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

        [HttpPost("{id}/Feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] String comment,[FromHeader] String token, [FromRoute] int id)
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
    }
}
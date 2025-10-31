using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{customerId}/complete/{menuId}")]
        public async Task<IActionResult> completeOrder([FromHeader] String token, [FromRoute] int customerId, [FromRoute] int menuId                                              )
        {
            try
            {
                var result = await _orderService.completeOrder(token, customerId, menuId);
                if (result == null) return BadRequest("No matching weekly menu order found.");
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
    }
}
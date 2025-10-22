using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> registerUser([FromBody] UserDTO userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if(result == null)
                return BadRequest("Email already exists");
            return Ok(result);
        }
    }
}
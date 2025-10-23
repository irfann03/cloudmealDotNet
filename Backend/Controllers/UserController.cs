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
        public readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> registerUser([FromBody] UserDTO userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (result == null)
                return BadRequest("Email already exists");
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserSessionDTO>> loginUser([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);
            if (result == null)
                return Unauthorized("Invalid email or password");
            return Ok(result);
        }

        [HttpPost("recharge")]
        public async Task<ActionResult<RechargeResponseDTO>> rechargeWallet([FromHeader] String token, [FromQuery] float amount)
        {
            Console.WriteLine("reieved amount: " + amount);
            try
            {
                var result = await _userService.RechargeWalletAsync(amount, token);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
        public Task<UserResponseDTO> RegisterUserAsync(UserDTO userDto);
    }
}
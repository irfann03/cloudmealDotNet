using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Configurations;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        public readonly MyDBContext _dbContext;

        public UserService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserResponseDTO> RegisterUserAsync(UserDTO userDto)
        {
            var existingUser = await _dbContext.Users.Where(u => u.Email == userDto.Email).FirstOrDefaultAsync();

            if (existingUser != null)
                 return null;

            var user = new Users
            {
                Email = userDto.Email,
                Password = userDto.Password,
                UserType = userDto.UserType,
                CreatedAt = DateTime.Now
            };

            switch (userDto.UserType)
            {
                case UserType.CUSTOMER:
                    if (userDto.Customer == null)
                        throw new ArgumentException("Customer data is required");

                    user.Customer = new Customer(userDto.Customer.Name, userDto.Customer.Phone);
                    break;

                case UserType.KITCHEN:
                    if (userDto.Kitchen == null)
                        throw new ArgumentException("Kitchen data is required");

                    user.Kitchen = new Kitchen(userDto.Kitchen.Name, userDto.Kitchen.Phone, userDto.Kitchen.Address, userDto.Kitchen.Longitude, userDto.Kitchen.Latitude);
                    break;

                case UserType.ADMIN:
                    break;
            }

            user.Wallet = new Wallet
            {
                Balance = 0,
                UserType = userDto.UserType
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                UserType = user.UserType
            };
        }
    }
}
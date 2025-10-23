using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Backend.Configurations;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MyDBContext _dbContext;
        public CustomerService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<String> AddAddressAsync(Address address, String token)
        {
            var session = await _dbContext.UserSession.FirstOrDefaultAsync(s => s.Token == token);
            if (session == null || session.EndTime < DateTime.Now)
            {
                return "Invalid or expired session token.";
            }

            if (!session.UserType.ToString().Equals("CUSTOMER", StringComparison.OrdinalIgnoreCase))
                return "Invalid session token.";

            var user = await _dbContext.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Id == session.UserId);

            if (user == null || user.Customer == null)
            {
                return "User not found or is not a customer.";
            }

            var newAddress = new Address
            {
                Details = address.Details,
                Longitude = address.Longitude,
                Latitude = address.Latitude,
                Customer = user.Customer,
                DefaultAddress = address.DefaultAddress,
                AddressType = address.AddressType
            };

            _dbContext.Address.Add(newAddress);
            await _dbContext.SaveChangesAsync();

            return "Address added successfully.";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Services
{
    public interface ICustomerService
    {
        public Task<String> AddAddressAsync(Address address, String token);
    }
}
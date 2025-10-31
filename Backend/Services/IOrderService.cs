using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IOrderService
    {
        public Task<String> completeOrder(String token, int customerId, int menuId);
    }
}
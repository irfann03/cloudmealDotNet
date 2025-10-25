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
        public Task<String> SubmitFeedbackAsync(string feedbackText, int MenuId, String token);
        public Task<IEnumerable<RechargeHistory>> GetRechargeHistoryAsync(String token);
    }
}
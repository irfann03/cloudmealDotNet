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
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;
        public CustomerService(MyDBContext dbContext, IAuthService authService, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _authService = authService;
            _httpClient = httpClient;
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

        public async Task<String> SubmitFeedbackAsync(string feedbackText, int MenuId, String token)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
            {
                return "Invalid or expired session token.";
            }

            if (session.UserType.ToString() != "CUSTOMER")
            {
                return "Only customers can submit feedback.";
            }

            var customer = await _dbContext.Customer
            .Where(c => c.UserId == session.UserId).FirstOrDefaultAsync();
            if (customer == null)
            {
                return "Customer not found.";
            }

            Console.WriteLine($"Looking for MenuId: {MenuId}");

            var menu = await _dbContext.Menu
            .Include(m => m.Kitchen).
            FirstOrDefaultAsync(m => m.MenuId == MenuId);
            if (menu == null)
            {
                return "Menu item not found.";
            }

            var apiUrl = "http://127.0.0.1:8000/analyze";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, new { comment = feedbackText });
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to analyze feedback");

            var analysis = await response.Content.ReadFromJsonAsync<FeedbackAnalysisResponse>();

            var feedback = new FeedBack
            {
                Description = feedbackText,
                Customer = customer,
                Menu = menu,
                Kitchen = menu.Kitchen,
                Sentiment = Enum.Parse<Sentiment>(analysis.sentiment, true),
                ComplaintArea = analysis.complaint_area != null
                    ? Enum.Parse<ComplaintArea>(analysis.complaint_area, true)
                    : ComplaintArea.QUALITY_ISSUE,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.FeedBack.Add(feedback);
            await _dbContext.SaveChangesAsync();
            return "Feedback submitted successfully.";
        }

        public async Task<IEnumerable<RechargeHistory>> GetRechargeHistoryAsync(String token)
        {
            var session = await _authService.ValidateTokenAsync(token);
            if (session == null)
            {
                throw new UnauthorizedAccessException("Invalid or expired session token.");
            }

            if (session.UserType.ToString() != "CUSTOMER")
            {
                throw new UnauthorizedAccessException("Only customers can view recharge history.");
            }

            var customer = await _dbContext.Customer
                .Include(c => c.PastRecharge)
                .Where(c => c.UserId == session.UserId).FirstOrDefaultAsync();

            if (customer == null) throw new ArgumentException("Customer not found.");
            
            return customer.PastRecharge;
        }
    }
}
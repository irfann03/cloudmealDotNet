using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Configurations;
using Microsoft.AspNetCore.Identity.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly MyDBContext _dbContext;

        public AuthService(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserSessionDTO?> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _dbContext.Users
           .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
                return null;

            string token = $"{Guid.NewGuid()}_{user.UserType}";

            var session = new UserSession
            {
                Token = token,
                UserType = user.UserType,
                UserId = user.Id,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };

            _dbContext.UserSession.Add(session);
            await _dbContext.SaveChangesAsync();

            return new UserSessionDTO
            {
                Token = token,
                UserType = user.UserType.ToString(),
                StartTime = session.StartTime,
                EndTime = session.EndTime
            };
        }

        public async Task<UserSession?> ValidateTokenAsync(string token)
        {
            
        var session = await _dbContext.UserSession.FirstOrDefaultAsync(s => s.Token == token);

        if (session == null)
            return null;

        if (session.EndTime <= DateTime.Now)
        {
            _dbContext.UserSession.Remove(session);
            await _dbContext.SaveChangesAsync();
            return null;
        }

        return session;
        }

    }
}
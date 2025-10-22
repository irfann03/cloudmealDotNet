using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Configurations
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Kitchen> Kitchen { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<RechargeHistory> RechargeHistory { get; set; }
        public DbSet<WeeklyMenu> WeeklyMenu { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }  
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<UserSession> UserSession { get; set; }
    }
}
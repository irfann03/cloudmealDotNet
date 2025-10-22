using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    [Index(nameof(Phone), IsUnique = true)]
    public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public string Name { get; set; }
    public string Phone { get; set; }

    public List<Address> Addresses { get; set; } = new();
    public List<RechargeHistory> PastRecharge { get; set; } = new();
    public List<WeeklyMenu> WeeklyMenu { get; set; } = new();
    public List<Orders> Orders { get; set; } = new();
    public List<FeedBack> Feedbacks { get; set; } = new();

    [ForeignKey("User")]
    public int? UserId { get; set; }
    public Users User { get; set; }
}
}
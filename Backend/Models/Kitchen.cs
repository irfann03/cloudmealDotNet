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
    public class Kitchen
    {
        [Key]
        public int KitchenId { get; set; }

        public string? Name { get; set; }
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        public List<Menu> Menus { get; set; } = new();
        public List<MenuItems> MenuItems { get; set; } = new();
        public List<Orders> Orders { get; set; } = new();
        public List<FeedBack> Feedbacks { get; set; } = new();

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public Users? User { get; set; }

        public Kitchen()
        {
        }   

        public Kitchen(string? name, string? phone, string? address, double longitude, double latitude)
        {
            Name = name;
            Phone = phone;
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
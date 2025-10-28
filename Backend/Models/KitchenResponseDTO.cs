using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class KitchenResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public KitchenResponseDTO() { }

        public KitchenResponseDTO(int id, string? name, string? phone, string? address, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
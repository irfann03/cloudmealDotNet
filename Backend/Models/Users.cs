using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum UserType { ADMIN, CUSTOMER, KITCHEN }

    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public UserType UserType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Wallet? Wallet { get; set; }
        public Customer? Customer { get; set; }
        public Kitchen? Kitchen { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        public float Balance { get; set; }
        public UserType UserType { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Users? User { get; set; }
    }
}
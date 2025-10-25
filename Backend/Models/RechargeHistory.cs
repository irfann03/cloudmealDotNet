using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public enum PaymentStatus { PENDING, SUCCESS, FAILED }
    public class RechargeHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        public float Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime InitiatedOn { get; set; } = DateTime.Now;

        [JsonIgnore]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
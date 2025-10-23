using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RechargeResponseDTO
    {
        public int TransactionId { get; set; }
        public float Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime InitiatedOn { get; set; }

    }
}
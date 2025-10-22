using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum OrderStatus { PLACED, CONFIRMED, CANCELLED, DELIVERED }

    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Kitchen")]
        public int KitchenId { get; set; }
        public Kitchen? Kitchen { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }

        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
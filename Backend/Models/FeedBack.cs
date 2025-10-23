using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum Sentiment { POSITIVE, NEGATIVE}
    public enum ComplaintArea { QUALITY_ISSUE, QUANTITY_ISSUE,HYGIENE_ISSUE, PACKING_ISSUE}

    public class FeedBack
    {
        [Key]
        public int FeedbackId { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }

        [ForeignKey("Kitchen")]
        public int KitchenId { get; set; }
        public Kitchen? Kitchen { get; set; }

        public Sentiment Sentiment { get; set; }
        public ComplaintArea ComplaintArea { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
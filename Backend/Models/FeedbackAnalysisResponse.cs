using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class FeedbackAnalysisResponse
    {
        public string? sentiment { get; set; }
        public string? complaint_area { get; set; }
    }
}
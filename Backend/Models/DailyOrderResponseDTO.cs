using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class DailyOrderResponseDTO
    {
        public int CustomerId { get; set; }
        public int MenuId { get; set; }

        public DailyOrderResponseDTO() { }

        public DailyOrderResponseDTO(int customerId, int menuId)
        {
            CustomerId = customerId;
            MenuId = menuId;
        }
    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceFeedback.Models.Domain.Request
{
    
    public class ProductInBag
    {
       
        public long ProductId { get; set; }
      
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}

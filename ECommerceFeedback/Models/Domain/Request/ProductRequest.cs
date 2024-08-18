using System.ComponentModel.DataAnnotations;

namespace ECommerceFeedback.Models.Domain.Request
{
    public class ProductRequest
    {     
        public string? Name { get; set; }
     
        public string? Description { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public long Price { get; set; }
       
        public string? Category { get; set; }
        
        public DateTime ExpiryDate { get; set; }
    }
}

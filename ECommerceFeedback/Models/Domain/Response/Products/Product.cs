
namespace ECommerceFeedback.Models.Domain.Response
{
   
    public class Products 
    {
        
        public int ProductId { get; set; }
       
        public string? Name { get; set; }
      
        public string? Description { get; set; }
      
        public long Price { get; set; }
     
        public string? Category { get; set; }
      
        public DateTime ExpiryDate { get; set; }
    }
}
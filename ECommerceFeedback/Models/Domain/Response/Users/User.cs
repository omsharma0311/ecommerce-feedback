namespace ECommerceFeedback.Models.Domain.Response
{
    
    public class User
    {
        
        public int UserId { get; set; }
        
        public string? Name { get; set; }
       
        public string? Email { get; set; }
     
        public string? ShippingAddress { get; set; }
      
        public string? BillingAddress { get; set; }
      
        public string? PaymentDetails { get; set; }
    }
}

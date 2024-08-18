namespace ECommerceFeedback.Models.Domain.Request
{
   
    public class UserRequest
    {
        
        public string? Name { get; set; }
        
        public string? Email { get; set; }
       
        public string? ShippingAddress { get; set; }
       
        public string? BillingAddress { get; set; }
       
        public string? PaymentDetails { get; set; }
    }
}

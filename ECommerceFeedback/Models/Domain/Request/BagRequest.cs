using ECommerceFeedback.Models.Domain.Request;

namespace ECommerceFeedback.Models.Domain.Request
{
    
    public class BagRequest
    {
        public BagRequest()
        {
            ProductInBag = new();
        }
            
        public long UserId { get; set; }

        public List<ProductInBag> ProductInBag { get; set; }
       
    }
}

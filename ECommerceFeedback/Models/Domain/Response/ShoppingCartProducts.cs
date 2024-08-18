using DomainModel = ECommerceFeedback.Models.Domain.Response;

namespace ECommerceFeedback.Models.Domain.Response
{
  
    public class ShoppingCartProducts
    {
     
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public DomainModel.Products? Product { get; set; }
    }
}

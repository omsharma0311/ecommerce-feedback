using ECommerceFeedback.Common;


namespace ECommerceFeedback.Models.Domain.Response
{

    public class UserShoppingCart : ApiResponse
    {
        public double TotalCost {get; set; } 

        public int UserId { get; set; }

        public User User { get; set; }

        public bool ProductPurchased { get; set; }

        public string? OrderId { get; set; }

     
        public List<ShoppingCartProducts>? ShoppingCartProducts { get; set; }
    }
}
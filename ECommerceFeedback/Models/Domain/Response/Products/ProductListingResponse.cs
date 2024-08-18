using ECommerceFeedback.Common;


namespace ECommerceFeedback.Models.Domain.Response
{
    public class ProductListingResponse : ApiResponse
    {
        public List<Products>? Products { get; set; } 
    }
}

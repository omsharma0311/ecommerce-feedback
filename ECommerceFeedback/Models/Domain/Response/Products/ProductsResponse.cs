using ECommerceFeedback.Common;


namespace ECommerceFeedback.Models.Domain.Response
{
    public class ProductsResponse : ApiResponse
    {
        public ProductsResponse()
        {
            Products = new();
        }

        /// <summary>
        /// Products
        /// </summary>
        public Products Products { get; set; }  
    }
}

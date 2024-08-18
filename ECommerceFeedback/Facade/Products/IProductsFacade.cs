
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;


namespace ECommerceFeedback.Facade.Products
{
    public interface IProductsFacade
    {
       
        Task<ProductListingResponse> ProductsListing(ProductsListingRequest productListingRequest, CancellationToken cancellation = default);

        Task<ProductsResponse> AddProduct(ProductRequest products, CancellationToken cancellation = default);

        Task<ProductsResponse> ProductDetails(long productId, CancellationToken cancellation = default);

    }
}

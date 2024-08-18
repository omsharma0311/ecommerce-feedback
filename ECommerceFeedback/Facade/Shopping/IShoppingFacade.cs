using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;

namespace ECommerceFeedback.Facade.Shopping
{
    public interface IShoppingFacade
    {
        Task<TextResponse> AddProductsToBag(BagRequest bagRequest, CancellationToken cancellation = default);

        Task<UserShoppingCart> GetOrderDetails(long userId, CancellationToken cancellation = default);

        Task<TextResponse> PurchaseProducts(long userId, CancellationToken cancellation = default);
       
        Task<OrderDetails> OrderDetails(long userId, CancellationToken cancellation = default);

        Task<TextResponse> MarkOrderDelivered(CancellationToken cancellation = default);
    }
}

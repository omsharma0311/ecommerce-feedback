using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Repository.BaseRepository;

namespace ECommerceFeedback.Repository.ShoppingRepository
{
    public interface IShoppingRepository : IRepository<UserCart>
    {
        Task<bool> AddProductsToBag(UserCart userCart);
        Task<UserCart> GetOrderDetails(long userId, bool productsPurchased);
        Task<bool> PurchaseProducts(long userId);
        Task<bool> MarkOrderDelivered();
    }
}

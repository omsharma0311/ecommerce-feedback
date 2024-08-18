using ECommerceFeedback.DBContext;
using ECommerceFeedback.Models;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFeedback.Repository.ShoppingRepository
{
    public class ShoppingRepository : Repository<UserCart>, IShoppingRepository
    {
        public ShoppingRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<bool> PurchaseProducts(long userId)
        {
            var response = _dataContext.UserShoppingCarts.SingleOrDefault(x => x.UserId == userId && x.OrderId == null);
            var recordAffected = 0;

            if (response != null)
            {
                response.ProductPurchased = true;
                response.PurchasedDate = DateTime.Now;
                response.OrderId = Guid.NewGuid().ToString();
                recordAffected = await _dataContext.SaveChangesAsync();
            }

            return recordAffected != 0;
        }

        public async Task<bool> MarkOrderDelivered()
        {
            var updateStausForProducts = _dataContext.UserShoppingCarts.Where(x => x.OrderId != null).ToListAsync();
            var recordAffected = 0;
            if (updateStausForProducts != null)
            {
                var resultSet = updateStausForProducts.Result.Where(i => DateTime.Now > i.AuditDate && i.OrderId != null).ToList();
                if (resultSet.Count > 0)
                {
                    foreach (var i in resultSet)
                    {
                        i.OrderStaus = "Delivered";
                    }
                    recordAffected = await _dataContext.SaveChangesAsync();
                }
            }
            return recordAffected != 0;
        }

        public async Task<UserCart> GetOrderDetails(long userId, bool productPurchased)
        {
            var response = await _dataContext.UserShoppingCarts
                                        .Include(y => y.User)
                                        .Include(y => y.ShoppingCartProducts)
                                        .ThenInclude(y => y.Products)
                                        .Where(u => u.UserId == userId && u.ProductPurchased == productPurchased)
                                        .FirstOrDefaultAsync();

            if (response != null)
            {
                return response;
            }
            return new UserCart();
        }

        public async Task<bool> AddProductsToBag(UserCart userCart)
        {
            await _dataContext.UserShoppingCarts.AddAsync(userCart);
            var rowAffected = await _dataContext.SaveChangesAsync();

            if (rowAffected > 0)
            {
                return true;
            }

            return false;
        }

    

        private IQueryable<Product> GetProducts(string? category)
        {
            if (category != null)
            {
                return _dataContext.Products.Where(x => x.Category.Equals(category));
            }
            return _dataContext.Products;
        }

      
    }
}

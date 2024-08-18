using ECommerceFeedback.Models;
using ECommerceFeedback.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFeedback.DBContext
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public DbSet<Product> Products { get; set; }
        public DbSet<UserCart> UserShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

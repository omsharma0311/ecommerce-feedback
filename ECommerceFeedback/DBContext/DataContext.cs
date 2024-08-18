using ECommerceFeedback.Models;
using ECommerceFeedback.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFeedback.DBContext
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserCart> UserShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.ShippingAddress).IsRequired();
                entity.Property(e => e.BillingAddress).IsRequired();
                entity.Property(e => e.PaymentDetails).IsRequired();
            });

            modelBuilder.Entity<UserCart>(entity =>
            {
                entity.HasKey(e => e.UserCartId);
                entity.Property(t => t.ProductPurchased).HasDefaultValue(false);
                entity.Property(e => e.OrderId).HasDefaultValueSql(null);
                entity.Property(e => e.OrderStaus).HasDefaultValueSql(null);
                entity.Property(t => t.AuditDate).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.PurchasedDate).HasDefaultValue(null);
                entity.HasOne(e => e.User).WithMany(e => e.UserCart).HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<ShoppingCartProducts>(entity =>
            {
                entity.HasKey(e => e.CartId);
                entity.Property(e => e.Quantity).IsRequired();
                entity.HasOne(e => e.UserCart).WithMany(e => e.ShoppingCartProducts).HasForeignKey(e => e.UserCartId);
                entity.HasOne(e => e.Products).WithMany(e => e.ShoppingCartProducts).HasForeignKey(e => e.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Price).IsRequired();
                entity.Property(t => t.Category).IsRequired();
                entity.Property(t => t.AuditDate).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.ExpiryDate).IsRequired();
                entity.Property(t => t.ActiveIndicator).HasDefaultValue(true);
            });

        }
    }
}

using ECommerceFeedback.Common;
using ECommerceFeedback.Models;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Repository.BaseRepository;

namespace ECommerceFeedback.Repository.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllProducts(List<Filter> Filter);

        Task<Product> AddProducts(Product product);

        Task<Product> ProductDetails(long productId);
   
    }
}

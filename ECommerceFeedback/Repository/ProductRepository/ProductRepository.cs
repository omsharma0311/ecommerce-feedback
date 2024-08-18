using ECommerceFeedback.Common;
using ECommerceFeedback.DBContext;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFeedback.Repository.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Product>> GetAllProducts(List<Filter> FilterCriteria)
        {
            var category = FilterCriteria.Find(x => x.Id.Equals(Constants.Category, StringComparison.OrdinalIgnoreCase))?.Values.FirstOrDefault();
            var priceOrderBy = FilterCriteria.Find(x => x.Id.Equals(Constants.PriceOrderBy, StringComparison.OrdinalIgnoreCase))?.Values.FirstOrDefault();

            if (priceOrderBy != null && priceOrderBy.ToLower().Equals(Constants.PriceHighToLow.ToLower(), StringComparison.OrdinalIgnoreCase))
            {
                return await GetProducts(category).OrderByDescending(x => x.Price).ToListAsync();
            }
            else if (priceOrderBy != null && priceOrderBy.ToLower().Equals(Constants.PriceLowTohigh.ToLower()))
            {
                return await GetProducts(category).OrderBy(x => x.Price).ToListAsync();
            }
            return await GetProducts(category).Where(x => DateTime.Now < x.ExpiryDate && x.ActiveIndicator == true).ToListAsync();
        }

        public async Task<Product> AddProducts(ECommerceFeedback.Models.Data.Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> ProductDetails(long productId)
        {
            return await _dataContext.Products.Where(x => x.ProductId.Equals(productId) && DateTime.Now < x.ExpiryDate && x.ActiveIndicator == true).FirstAsync();
        }
        

        private IQueryable<Product> GetProducts(string? category)
        {
            if (category != null)
            {
                return _dataContext.Products.Where(x => x.Category.ToLower().Equals(category.ToLower()) && DateTime.Now < x.ExpiryDate && x.ActiveIndicator == true);
            }
            return _dataContext.Products;
        }

 
    }
}

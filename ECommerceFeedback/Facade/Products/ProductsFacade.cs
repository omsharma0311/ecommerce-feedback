using AutoMapper;
using ECommerceFeedback.Common;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Repository.ProductRepository;
using Serilog;
using ILogger = Serilog.ILogger;



namespace ECommerceFeedback.Facade.Products
{
    public class ProductsFacade : IProductsFacade
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.Logger.ForContext<ProductsFacade>();
        public IProductRepository _productRepository;

        public ProductsFacade(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       
        public async Task<ProductListingResponse> ProductsListing(ProductsListingRequest productListingRequest, CancellationToken cancellation = default)
        {
            ProductListingResponse productListingResponse = new();
            var request = PrepareFilterCritera(productListingRequest);
            var allProducts =  await _productRepository.GetAllProducts(request);
            productListingResponse.Products = _mapper.Map<List<Models.Domain.Response.Products>>(allProducts);
            return productListingResponse;
        }


        public async Task<ProductsResponse> AddProduct(ProductRequest addProducts, CancellationToken cancellation = default)
        {
            var request = PrepareAddProductRequest(addProducts);
            var product = await _productRepository.AddProducts(request);
            ProductsResponse productsResponse = new();
            productsResponse.Products = _mapper.Map<Models.Domain.Response.Products>(product);

            return productsResponse;
        }


        public async Task<ProductsResponse> ProductDetails(long productId, CancellationToken cancellation = default)
        {
            var products = await _productRepository.ProductDetails(productId);
            ProductsResponse productsResponse = new();

            productsResponse.Products = _mapper.Map<Models.Domain.Response.Products>(products);

            return productsResponse;
        }

   
        private static List<Filter> PrepareFilterCritera(ProductsListingRequest productListingRequest)
        {
            var filterCriteria = new List<Filter> {
                                                      new Filter {
                                                        Id = nameof(productListingRequest.Category),
                                                        Values = productListingRequest.Category != null && productListingRequest.Category.Any()
                                                        && !string.IsNullOrEmpty(productListingRequest.Category)
                                                        ?  new List<string> () { productListingRequest.Category }
                                                        : new List<string>()
                                                      },
                                                      new Filter {
                                                        Id = nameof(productListingRequest.PriceOrderBy),
                                                        Values = productListingRequest.PriceOrderBy != null && productListingRequest.PriceOrderBy.Any()
                                                        && !string.IsNullOrEmpty(productListingRequest.PriceOrderBy)
                                                        ?  new List<string> () { productListingRequest.PriceOrderBy }
                                                        : new List<string>()
                                                      }
                                          };
            return filterCriteria;
        }
       
        private static Product PrepareAddProductRequest(ProductRequest products)
        {
            return new Product()
            {
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                Category = products.Category
            };
        }

    }
}

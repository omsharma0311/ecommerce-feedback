using AutoMapper;
using ECommerceFeedback.Common;
using ECommerceFeedback.Models.Data;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Repository.ProductRepository;
using ECommerceFeedback.Repository.ShoppingRepository;
using Serilog;
using ILogger = Serilog.ILogger;


namespace ECommerceFeedback.Facade.Shopping
{
    public class ShoppingFacade : IShoppingFacade
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.Logger.ForContext<ShoppingFacade>();
        public IShoppingRepository _shoppingRepository;
        public IProductRepository _productRepository;
        public TextResponse textResponse = new();

        public ShoppingFacade(IShoppingRepository shoppingRepository, IProductRepository productRepository, IMapper mapper)
        {
            _shoppingRepository = shoppingRepository ?? throw new ArgumentNullException(nameof(shoppingRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserShoppingCart> GetOrderDetails(long userId, CancellationToken cancellation = default)
        {
            var userCart = await _shoppingRepository.GetOrderDetails(userId, false);
            UserShoppingCart userShoppingBag = new();
            userShoppingBag = _mapper.Map<UserShoppingCart>(userCart);

            double totalCost = 0;

            var productsDetails = userShoppingBag.ShoppingCartProducts.Select(x => new { x.ProductId, x.Quantity, x.Product.Price }).ToList();

            foreach (var i in productsDetails)
            {
                totalCost += (i.Quantity * i.Price);
            }
            userShoppingBag.TotalCost = totalCost;
            return userShoppingBag;
        }

        public async Task<TextResponse> AddProductsToBag(BagRequest bagRequest, CancellationToken cancellation = default)
        {
            var request = PrepareAddToBagRequest(bagRequest);
            var response = await _shoppingRepository.AddProductsToBag(request);
            
            if(response)
                textResponse.Text = Constants.ProductsAddedSuccessfully;
            else
                textResponse.Text = Constants.ProductsAddedSuccessfully;

            return textResponse;
        }

    
        public async Task<TextResponse> PurchaseProducts(long userId, CancellationToken cancellation = default)
        {
            var response = await _shoppingRepository.PurchaseProducts(userId);

            if (response)
                textResponse.Text = Constants.ProductsPurchased;
            else
                textResponse.Text = Constants.ProductsNotPurchased;
            return textResponse;
        }

        public async Task<TextResponse> MarkOrderDelivered(CancellationToken cancellation = default)
        {
            var response = await _shoppingRepository.MarkOrderDelivered();

            if (response)
                textResponse.Text = Constants.DeliverySatusUpdated;
            else
                textResponse.Text = Constants.DeliverySatusNotUpdated;
            return textResponse;
        }

  
        public async Task<OrderDetails> OrderDetails(long userId, CancellationToken cancellation = default)
        {
            var userCart = await _shoppingRepository.GetOrderDetails(userId, true);

            UserShoppingCart userShoppingBag = new();
            userShoppingBag = _mapper.Map<UserShoppingCart>(userCart);

            double totalCost = 0;

            var dt = userShoppingBag.ShoppingCartProducts.Select(x => new { x.ProductId, x.Quantity, x.Product.Price }).ToList();

            foreach (var i in dt)
            {
                totalCost += (i.Quantity * i.Price);
            }

            OrderDetails orderDetails = new();
            orderDetails.TotalCost = (long)totalCost;
            orderDetails.Text = Constants.OrderConfirmed;
            orderDetails.OrderId = userShoppingBag.OrderId;

            return orderDetails;
        }

     

        private UserCart PrepareAddToBagRequest(BagRequest bagRequest)
        {
            return new UserCart()
            {
                UserId = bagRequest.UserId,
                ShoppingCartProducts = ProductsInShoppingBag(bagRequest)
            };
        }

        private static List<Models.Data.ShoppingCartProducts> ProductsInShoppingBag(BagRequest bagRequest)
        {
            var productInBag = new List<Models.Data.ShoppingCartProducts>();
            foreach ( var product in bagRequest.ProductInBag)
            {
                productInBag.Add(new Models.Data.ShoppingCartProducts()
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                });
            }
            return productInBag;
        }

    


    }
}

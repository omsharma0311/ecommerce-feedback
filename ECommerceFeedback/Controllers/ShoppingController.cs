using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Net.Mime;
using ECommerceFeedback.Common;
using ILogger = Serilog.ILogger;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ECommerceFeedback.Facade.Shopping;
using ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Models.Domain.Request;

namespace ECommerceFeedback.Controllers
{
    [ApiController]
    [Route(Constants.ApiPrefix + "[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ShoppingController : BaseApiController
    {
        private readonly IShoppingFacade _shoppingFacade;
        private readonly ILogger _logger = Log.Logger.ForContext<ShoppingController>();
        private readonly Validation _validation;

        public ShoppingController(Validation validator, IShoppingFacade shoppingFacade)
        {
            _validation = validator;
            _shoppingFacade = shoppingFacade;
        }

     
    
        [HttpPost]
        [Route("addProductsToBag")]
        [ProducesResponseType(typeof(TextResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"UserShopping-{nameof(AddToBag)}", Tags = new string[] { "UserShopping" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> AddToBag(BagRequest bagRequest)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Add To Bag | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var message = string.Empty;

            if (_validation.ValidateAddToBagRequest(bagRequest.ProductInBag, ref message))
            {
                stopwatch.Stop();
                _logger.Information("PERF-CONTROLLER | Validation Failed - Add Products | Time: {0}ms", stopwatch.ElapsedMilliseconds);
                _logger.Information("PERF-CONTROLLER | Validation Failed - Add Products | Time: {0}ms", message);
                return BadRequest(message);
            }
    
            var response = await _shoppingFacade.AddProductsToBag(bagRequest);
           
            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Add To Bag | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

      
        [HttpGet]
        [Route("getShoppingBag")]
        [ProducesResponseType(typeof(UserShoppingCart), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"UserShopping-{nameof(GetShoppingBag)}", Tags = new string[] { "UserShopping" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> GetShoppingBag(long userId)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Get Shopping Bag | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _shoppingFacade.GetOrderDetails(userId);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Get Shopping Bag | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

       
        [HttpPut]
        [Route("purchaseProducts")]
        [ProducesResponseType(typeof(TextResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"UserShopping-{nameof(PurchaseProducts)}", Tags = new string[] { "UserShopping" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> PurchaseProducts(long userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Purchase Products | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _shoppingFacade.PurchaseProducts(userId);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Purchase Products | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

     
        [HttpGet]
        [Route("orderDetails")]
        [ProducesResponseType(typeof(OrderDetails), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"UserShopping-{nameof(OrderDetails)}", Tags = new string[] { "UserShopping" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> OrderDetails(long userId)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Order Details | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _shoppingFacade.OrderDetails(userId);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Order Details | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

       
        [HttpGet]
        [Route("updateOrderDeliveryStatus")]
        [ProducesResponseType(typeof(TextResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"UserShopping-{nameof(UpdateOrderDeliveryStatus)}", Tags = new string[] { "UserShopping" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> UpdateOrderDeliveryStatus()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Update Delivery Status | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _shoppingFacade.MarkOrderDelivered();

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Update Delivery Status | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

      

    }
}

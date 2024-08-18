using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Net.Mime;
using ILogger = Serilog.ILogger;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using ECommerceFeedback.Common;
using ECommerceFeedback.Facade.Products;
using ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Models.Domain.Request;


namespace ECommerceFeedback.Controllers
{
    [ApiController]
    [Authorize("admin:mainuser")]
    [Route(Constants.ApiPrefix + "[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProductsController : BaseApiController
    {
        private readonly IProductsFacade _productsFacade;
        private readonly ILogger _logger = Log.Logger.ForContext<ProductsController>();
        private readonly Validation _validation;

       
        public ProductsController(Validation validator, IProductsFacade productFacade)
        {
            _validation = validator;
            _productsFacade = productFacade;
        }


       
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"Products-{nameof(AddProduct)}", Tags = new string[] { "Products" }, Description = "Add the products with its details: Name, Price, Description and Category.")]
        public async Task<IActionResult> AddProduct(ProductRequest productRequest)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Information("PERF-CONTROLLER | Start Process - Add Products | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var message = string.Empty;
            if (_validation.ValidateAddProductRequest(productRequest.Name, productRequest.Price, productRequest.Category, productRequest.ExpiryDate, ref message))
            {
                stopwatch.Stop();
                _logger.Information("PERF-CONTROLLER | Validation Failed - Add Products | Time: {0}ms", stopwatch.ElapsedMilliseconds);
                _logger.Information("PERF-CONTROLLER | Validation Failed - Add Products | Time: {0}ms", message);
                return BadRequest(message);
            }

            var response = await _productsFacade.AddProduct(productRequest);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Finished Process - Products | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

     

     

      
        [HttpPost]
        [Route("listing")]
        [ProducesResponseType(typeof(ProductListingResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"Products-{nameof(ProductsListing)}", Tags = new string[] { "Products" },  Description = "It list out all the products with their details like ProductId, Name, Price, Description, Category and Expiry Dates.")]
        public async Task<IActionResult> ProductsListing(ProductsListingRequest productsListingRequest)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            _logger.Information("PERF-CONTROLLER | Start Process - Fetch Products List | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var message = string.Empty;
            if (_validation.ValidateProductsListingRequest(productsListingRequest.Category, productsListingRequest.PriceOrderBy, ref message))
            {
                stopwatch.Stop();               
                _logger.Information("PERF-CONTROLLER | Validation Failed - Fetch Products List | Time: {0}ms", stopwatch.ElapsedMilliseconds);
                _logger.Information("PERF-CONTROLLER | Validation Failed - Fetch Products List | Time: {0}ms", message);
                return BadRequest(message);
            }

            var response = await _productsFacade.ProductsListing(productsListingRequest);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Finished Process - Fetched Products Listing | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);   
        }

   

       

        [HttpGet]
        [Route("details")]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"Products-{nameof(ProductDetails)}", Tags = new string[] { "Products" }, Description = " Fetch product details like ProductId, Name, Price, Description, Category and Expiry Dates.")]
        public async Task<IActionResult> ProductDetails(long productId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.Information("PERF-CONTROLLER |  Start Process - Fetch Products Details | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _productsFacade.ProductDetails(productId);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Finished Process - Products Details | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }

    

    }
}

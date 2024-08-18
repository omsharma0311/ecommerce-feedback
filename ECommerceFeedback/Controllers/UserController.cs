using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Net.Mime;
using ILogger = Serilog.ILogger;
using Swashbuckle.AspNetCore.Annotations;
using ECommerceFeedback.Common;
using ECommerceFeedback.Facade.User;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;
using System.Net;


namespace ECommerceFeedback.Controllers
{
    [ApiController]
    [Route(Constants.ApiPrefix + "[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController : BaseApiController
    {
        private readonly IUserFacade _userFacade;
        private readonly ILogger _logger = Log.Logger.ForContext<UserController>();
        private readonly Validation _validation;

       
        public UserController(Validation validation, IUserFacade userFacade)
        {
            _validation = validation;
            _userFacade = userFacade;
        }

       

        
        [HttpPost]
        [Route("addUser")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"User-{nameof(AddUser)}", Tags = new string[] { "User" }, Description = "API for fetching all list of users.")]
        public async Task<IActionResult> AddUser(UserRequest userRequest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Insert Users | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _userFacade.AddUser(userRequest);

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | Add Products | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }



        [HttpPost]
        [Route("userListing")]
        [ProducesResponseType(typeof(UserListingResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"User-{nameof(UserListing)}", Tags = new string[] { "User" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> UserListing()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Fetch User List | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            var response = await _userFacade.UserListing();

            stopwatch.Stop();
            _logger.Information($"PERF-CONTROLLER | User Listing | Time: {stopwatch.ElapsedMilliseconds} ms");
            return Respond(response);
        }



        [HttpGet]
        [Route("userDetails")]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = $"User-{nameof(UserDetails)}", Tags = new string[] { "User" }, Description = "API for fetching all list of products.")]
        public async Task<IActionResult> UserDetails(long userId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _logger.Debug("PERF-CONTROLLER | Fetch User Details | Time: {0}ms", stopwatch.ElapsedMilliseconds);

            //var response = await _userFacade.UserDetails(userId);

            //stopwatch.Stop();
            //_logger.Information($"PERF-CONTROLLER | User Details | Time: {stopwatch.ElapsedMilliseconds} ms");
            //return Respond(response);

            try
            {
                var response = await _userFacade.UserDetails(userId);
                stopwatch.Stop();
                _logger.Information($"PERF-CONTROLLER | User Details | Time: {stopwatch.ElapsedMilliseconds} ms");
                return Respond(response);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.Error(ex, "An error occurred while fetching user details.");
                _logger.Information($"PERF-CONTROLLER | User Details | Failed | Time: {stopwatch.ElapsedMilliseconds} ms");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.");
            }
        }



    }
}
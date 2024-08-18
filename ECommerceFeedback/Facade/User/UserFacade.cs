using AutoMapper;
using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Repository.UserRepository;
using Serilog;
using ILogger = Serilog.ILogger;


namespace ECommerceFeedback.Facade.User
{
    public class UserFacade : IUserFacade
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.Logger.ForContext<UserFacade>();
        public IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserListingResponse> UserListing(CancellationToken cancellation = default)
        {
            UserListingResponse userListingResponse = new();
            var allUsers = await _userRepository.GetAllUsers();
            var response = _mapper.Map<List<Models.Domain.Response.User>>(allUsers);
            userListingResponse.User = response;
            return userListingResponse;
        }

       
        public async Task<UserResponse> AddUser(UserRequest addUser, CancellationToken cancellation = default)
        {
            var request = PrepareAddUserRequest(addUser);
            var user = await _userRepository.AddUser(request);
            UserResponse userResponse = new();
            userResponse.User = _mapper.Map<Models.Domain.Response.User>(user);
            return userResponse;
        }

        public async Task<UserResponse> UserDetails(long userId, CancellationToken cancellation = default)
        {
            var user = await _userRepository.UserDetails(userId);
            //Models.Data.User userData = new Models.Data.User();
            UserResponse userResponse = new();
            userResponse.User = _mapper.Map<Models.Domain.Response.User>(user);
            return userResponse;
        }

      
               
        private Models.Data.User PrepareAddUserRequest(UserRequest user)
        {
            return new Models.Data.User()
            {
                Name = user.Name,
                Email = user.Email,
                ShippingAddress = user.ShippingAddress,
                BillingAddress = user.BillingAddress,
                PaymentDetails = user.PaymentDetails
            };
        }

       


    }
}

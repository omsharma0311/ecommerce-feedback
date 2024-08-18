using ECommerceFeedback.Models.Domain.Request;
using ECommerceFeedback.Models.Domain.Response;

namespace ECommerceFeedback.Facade.User
{
    public interface IUserFacade
    {
       
        Task<UserListingResponse> UserListing(CancellationToken cancellation = default);

       
        Task<UserResponse> AddUser(UserRequest addUser, CancellationToken cancellation = default);

      
        Task<UserResponse> UserDetails(long userId, CancellationToken cancellation = default);

    }
}

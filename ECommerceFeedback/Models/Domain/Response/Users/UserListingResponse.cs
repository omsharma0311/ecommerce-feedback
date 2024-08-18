using ECommerceFeedback.Common;

namespace ECommerceFeedback.Models.Domain.Response
{
    public class UserListingResponse : ApiResponse
    {
        public List<User>? User { get; set; } 
    }
}

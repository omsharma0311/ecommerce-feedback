

using ECommerceFeedback.Common;

namespace ECommerceFeedback.Models.Domain.Response
{
    public class UserResponse : ApiResponse
    {
        public UserResponse()
        {
            User = new();
        }

        public User User { get; set; }  
    }
}

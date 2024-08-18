using ECommerceFeedback.Models;
using DomainModel = ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Repository.BaseRepository;
using ECommerceFeedback.Models.Data;

namespace ECommerceFeedback.Repository.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> AddUser(User user);

        Task<User> UserDetails(long userId);
    }
}

using ECommerceFeedback.DBContext;
using DomainModel=ECommerceFeedback.Models.Domain.Response;
using ECommerceFeedback.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using ECommerceFeedback.Models.Data;


namespace ECommerceFeedback.Repository.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UserDetails(long userId)
        {
            return await _dataContext.Users.Where(x => x.UserId.Equals(userId)).FirstOrDefaultAsync();
        }

    }
}

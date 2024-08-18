using ECommerceFeedback.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFeedback.Repository.BaseRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }
        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

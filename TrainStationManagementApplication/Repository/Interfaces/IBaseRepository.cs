using System.Linq.Expressions;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> GetById(string id);
        public Task<T> GetByAny(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllAsync();
        public T Update(T entity);
        public Task Delete(T entity);
        public Task SaveChangesAsync();
        public void SaveChanges();
    }
}

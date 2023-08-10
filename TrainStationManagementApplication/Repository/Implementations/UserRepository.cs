using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }
        public async Task<User> CreateAsync(User user)
        {
            await _applicationContext.Users.AddAsync(user);
            await _applicationContext.SaveChangesAsync();
            return user;
        }

        public async Task Delete(User user)
        {
             _applicationContext.SaveChangesAsync();
        }

        public async Task<bool> ExistByEmail(string userEmail)
        {
            return await _applicationContext.Users.AnyAsync(u => u.Email.Equals(userEmail));
        }

        public async Task<bool> ExistById(string userId)
        {
            return await _applicationContext
                .Users
                .AnyAsync(u => u.Id.Equals(userId));
        }

        public async Task<bool> ExistByPassword(string password)
        {
            return await _applicationContext
                .Users
                .AnyAsync(u => u.Password.Equals(password));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _applicationContext
                .Users
                .Include(u => u.Passenger)
                .Include(u => u.Admin)
                .Include(u => u.Address)
                .Where(u => u.IsDeleted.Equals(false))
                .ToListAsync();
            return users;
        }

        public async Task<User> GetByAny(Expression<Func<User, bool>> expression)
        {
            var newExpression = await _applicationContext
                .Users
                .Include(u => u.Passenger)
                .Include(u => u.Admin)
                .Include(u => u.Address)
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<User> GetById(string id)
        {
            var user = await _applicationContext
                 .Users
                 .Include(u => u.Passenger)
                 .Include(u => u.Admin)
                 .Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Id.Equals(id) && u.IsDeleted.Equals(false));
            return user;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public User Update(User user)
        {
            _applicationContext.SaveChanges();
            return user;
        }
    }

       

}


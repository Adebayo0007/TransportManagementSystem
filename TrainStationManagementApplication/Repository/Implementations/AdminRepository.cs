using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _applicationContext;

        public AdminRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<Admin> CreateAsync(Admin admin)
        {
            await _applicationContext.Admins.AddAsync(admin);
            await _applicationContext.SaveChangesAsync();
            return admin;
        }

        public async Task Delete(Admin admin)
        {
            _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            var admins = await _applicationContext
                .Admins
                .Include(a => a.User)
                .ThenInclude(a => a.Address)
                .Where(a => a.User.IsDeleted.Equals(false))
                .ToListAsync();
            return admins;
        }

        public async Task<Admin> GetById(string id)
        {
            var admin = await _applicationContext
                .Admins
                .Include(a => a.User)
                .ThenInclude(a => a.Address)
                .Where(a => a.Id.ToLower().Equals(id.ToLower()))
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task<Admin> GetByAny(Expression<Func<Admin, bool>> expression)
        {

            var newExpression = await _applicationContext
                .Admins
                .Include(a => a.User)
                .ThenInclude(a => a.Address)
                .Where(a => a.User.IsDeleted.Equals(false))
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Admin Update(Admin admin)
        {
            _applicationContext.SaveChanges();
            return admin;
        }
    }
}

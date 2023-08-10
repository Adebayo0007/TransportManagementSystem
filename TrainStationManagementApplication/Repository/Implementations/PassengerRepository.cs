using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PassengerRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Passenger> CreateAsync(Passenger passenger)
        {
            await _applicationContext.Passengers.AddAsync(passenger);
            await _applicationContext.SaveChangesAsync();
            return passenger;
        }

        public async Task Delete(Passenger passenger)
        {
            _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
           var passengers = await _applicationContext.Passengers
                .Include(p => p.User)
                .ThenInclude(p => p.Address)
                .Include(p => p.Trips)
                .Include(p => p.Transactions)
                .Where(p => p.User.IsDeleted == false)
                .ToListAsync();          
                return passengers;
        }


        public async Task<Passenger> GetByAny(Expression<Func<Passenger, bool>> expression)
        {
            var newExpression = await _applicationContext
                .Passengers
                .Include(p => p.User)
                .ThenInclude(p => p.Address)
                .Include(p => p.Trips)
                .Include(p => p.Transactions)
                .Where(p => p.User.IsDeleted.Equals(false))
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<Passenger> GetById(string id)
        {
            var Passenger = await _applicationContext
                .Passengers
                .Include(p => p.User)
                .ThenInclude(p => p.Address)
                .Include(p => p.Trips)
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.User.Id.Equals(id) || p.Id.Equals(id) && p.User.IsDeleted.Equals(false));
            return Passenger;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Passenger Update(Passenger Passenger)
        {
            _applicationContext.SaveChanges();
            return Passenger;
        }
    }
}

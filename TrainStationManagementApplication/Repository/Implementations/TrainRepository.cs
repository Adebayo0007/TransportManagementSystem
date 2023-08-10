using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class TrainRepository : ITrainRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TrainRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<Train> CreateAsync(Train train)
        {
            await _applicationContext.Trains.AddAsync(train);
            await _applicationContext.SaveChangesAsync();
            return train;
        }

        public async Task Delete(Train train)
        {
            _applicationContext.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string trainId)
        {
            return await _applicationContext
                .Trains
                .AnyAsync(u => u.Id.Equals(trainId));
        }

        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            var trains = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)
                .Where(t => t.IsAvailable.Equals(false) || t.IsAvailable.Equals(true))

                .ToListAsync();
            return trains;
        }

        public async Task<IEnumerable<Train>> GetAvailableTrains()
        {
            var trains = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)
                .Where(t => t.IsAvailable.Equals(true) && t.AvailableSpace > 0 && t.IsDeleted.Equals(false))
                .ToListAsync();
            return trains;
        }
        public async Task<IEnumerable<Train>> GetUnAvailableTrains()
        {
            var trains = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)
                .Where(t => t.IsAvailable.Equals(false) && t.IsDeleted.Equals(false))
                .ToListAsync();
            return trains;

        }

        public async Task<Train> GetByAny(Expression<Func<Train, bool>> expression) 
        {
            var newExpression = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)
                .Where(t => t.IsDeleted.Equals(false))
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<Train> GetById(string id)
        {
            var train = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)               
                .FirstOrDefaultAsync(t => t.Id.Equals(id) && t.IsAvailable && !t.IsDeleted);
            return train;
        }
        public async Task<Train> GetByIdToUpdateBackToAvailable(string trainId)
        {
             var train = await _applicationContext
                .Trains               
                .FirstOrDefaultAsync(t => t.Id.Equals(trainId) && t.IsAvailable.Equals(false));
            return train;

        }
        public async Task<Train> GetTrainAfterBeingNonAvailable(string trainId)
        {
            var train = await _applicationContext
                .Trains
                .Include(t => t.Transactions)
                .Include(t => t.Route)
                .Include(t => t.Trips)
                .FirstOrDefaultAsync(t => t.Id.Equals(trainId) && !t.IsDeleted);
            return train;

        }
        public async Task<IEnumerable<Train>> GetTrainsByName(string name)
        {
            var trains = _applicationContext
                .Trains
                .Include(t => t.Route)
                .Where(t => t.Name == name && t.IsDeleted.Equals(false) && t.IsAvailable.Equals(true));
            return trains;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Train Update(Train train)
        {
            _applicationContext.SaveChanges();
            return train;
        }
    }
}

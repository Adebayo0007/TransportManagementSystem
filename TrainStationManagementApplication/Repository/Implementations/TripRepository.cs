using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TripRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }
        public async Task<Trip> CreateAsync(Trip trip)
        {
            await _applicationContext.Trips.AddAsync(trip);
            await _applicationContext.SaveChangesAsync();
            return trip;
        }

        public async Task Delete(Trip trip)
        {
            _applicationContext.Remove(trip);
            _applicationContext.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string tripId)
        {
            return await _applicationContext
                .Trips
                .AnyAsync(u => u.Id.Equals(tripId));
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            var trips = await _applicationContext
                .Trips
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .ThenInclude(t => t.Route)
                .Where(t => t.IsDeleted.Equals(false))
                .ToListAsync();
            return trips;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsOfATrain(string trainNumber)
        {
            var trips = await _applicationContext
                .Trips
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .ThenInclude(t => t.Route)
                .Where(t => t.IsDeleted.Equals(false) && t.Train.TrainNumber.Equals(trainNumber))
                .ToListAsync();
            return trips;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsOfToday()
        {
            var trips = await _applicationContext
                 .Trips
                 .Include(t => t.Passenger)
                 .Include(t => t.Train)
                 .ThenInclude(t => t.Route)
                 .Where(t => t.IsDeleted.Equals(false))
                 .ToListAsync();
            return trips;
        }

        public async Task<IEnumerable<Trip>> GetAllTripsOfTrainForParticularDay(DateTime date, string trainNumber)
        {
            var trips = await _applicationContext
                .Trips
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .ThenInclude(t => t.Route)
                .Where(t => t.IsDeleted.Equals(false) &&
                t.DateCreated.Equals(date) &&
                t.TrainNumber.Equals(trainNumber))
                .ToListAsync();
            return trips;
        }

        public async Task<Trip> GetByAny(Expression<Func<Trip, bool>> expression)
        {
            var newExpression = await _applicationContext
                .Trips
                .Include(t => t.Passenger)
                .Include(t => t.Train)
                .ThenInclude(t => t.Route)
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<Trip> GetById(string id)
        {
            var trip = await _applicationContext
               .Trips
               .Include(t => t.Passenger)
               .Include(t => t.Train)
               .ThenInclude(t => t.Route)
               .FirstOrDefaultAsync(t => t.Id.Equals(id) && t.IsDeleted.Equals(false));
            return trip;
        }
        public async Task<IEnumerable<Trip>> GetAllTripsOfToSendEmail(string trainNumber)
        {
          return await _applicationContext.Trips.Include(t => t.Passenger).ThenInclude(t => t.User).Where(t => t.TrainNumber == trainNumber).ToListAsync();

        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Trip Update(Trip trip)
        {
            _applicationContext.SaveChanges();
            return trip;
        }
       
    }
}

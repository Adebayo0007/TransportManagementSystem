using System.Linq.Expressions;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface ITrainRepository : IBaseRepository<Train>
    {
        public Task<bool> ExistById(string trainId);
        public Task<Train> GetTrainAfterBeingNonAvailable(string trainId);
        public Task<IEnumerable<Train>> GetTrainsByName(string name);
        public Task<Train> GetByIdToUpdateBackToAvailable(string trainId);
        public Task<IEnumerable<Train>> GetAvailableTrains();
        public Task<IEnumerable<Train>> GetUnAvailableTrains();
    }
}

using System.Linq.Expressions;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        public Task<IEnumerable<Trip>> GetAllTripsOfToday();
        public Task<IEnumerable<Trip>> GetAllTripsOfToSendEmail(string trainNumber);
        public Task<IEnumerable<Trip>> GetAllTripsOfATrain(string trainNumber);
        public Task<IEnumerable<Trip>> GetAllTripsOfTrainForParticularDay(DateTime date,string trainNumber);
        public Task<bool> ExistById(string tripId);
    }
}

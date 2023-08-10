using System.Linq.Expressions;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public Task<IEnumerable<Transaction>> GetAllTransactionsOfAPassenger(string passengerId);
        public Task<IEnumerable<Transaction>> GetAllTransactionsOfTrainForAParticularDate(string trainNumber, DateTime date);
    }
}

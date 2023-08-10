using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface ITransactionService
    {
        public Task CreateTransaction(string trainId);
        public Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactions();
        public Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactionsOfAPassenger(string passengerId);
        public Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactionsOfTrainForAParticularDate(string trainNumber, DateTime date);
        public Task<BaseResponse<TransactionDto>> GetTransaction(string id);
        public Task<BaseResponse<TransactionDto>> UpdateTransaction(string id, UpdateTransactionRequestModel model);
        public Task<BaseResponse<TransactionDto>> DeleteTransaction(string id);
    }
}

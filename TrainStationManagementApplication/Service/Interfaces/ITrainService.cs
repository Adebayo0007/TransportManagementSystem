using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface ITrainService
    {
        public Task<BaseResponse<TrainDto>> CreateTrain(CreateTrainRequestModel model);
        public Task<BaseResponse<IEnumerable<TrainDto>>> GetAllTrain();
        public Task<BaseResponse<IEnumerable<TrainDto>>> GetAvailableTrains();
        public Task<BaseResponse<IEnumerable<TrainDto>>> GetUnAvailableTrains();
        public Task<BaseResponse<TrainDto>> GetTrain(string id);
        public Task<BaseResponse<TrainDto>> GetTrainAfterBeingUnAvailable(string trainId);

        public Task<BaseResponse<TrainDto>> GetTrainByNumber(string trainNumber);
        public Task<BaseResponse<TrainDto>> UpdateTrain(string id, UpdateTrainRequestModel model);
        public Task UpdateTrainBackToAvailable(string trainId);
        public Task<BaseResponse<TrainDto>> DeleteTrain(string id);
        public Task<BaseResponse<TrainDto>> UpdateTrainToIsAvailable(string trainNumber);
        public Task<BaseResponse<TrainDto>> UpdateTrainToNotAvailable(string trainNumber);
        public Task<BaseResponse<IEnumerable<TrainDto>>> GetTrainsByName(string name);
    }
}

using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface IPassengerService
    {
        public Task<BaseResponse<PassengerDto>> CreatePassenger(CreatePassengerRequestModel model);
        public Task<BaseResponse<IEnumerable<PassengerDto>>> GetAllPassengers();
        public Task<BaseResponse<PassengerDto>> GetPassenger(string id);
        public Task<BaseResponse<PassengerDto>> UpdatePassenger(string id, UpdatePassengerRequestModel model);
        public Task<BaseResponse<PassengerDto>> DeletePassenger(string id);
    }
}

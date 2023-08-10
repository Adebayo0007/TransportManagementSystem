using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface ITripService
    {
        public Task<BaseResponse<TripDto>> CreateTrip(CreateTripRequestModel model);
        public Task<byte[]> GenerateReceipt(string passengerName, decimal amountPaid, DateTime paymentDate);
        public Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfToday();
        public Task<BaseResponse<IEnumerable<TripDto>>> GetAllTrips();
        public Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfATrain(string trainNumber);
        public Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfTrainForParticularDay(DateTime date, string trainNumber);
        public Task<BaseResponse<TripDto>> GetTrip(string id);
        public Task<BaseResponse<TripDto>> UpdateTrip(string id, UpdateTripRequestModel model);
        public Task<BaseResponse<TripDto>> DeleteTrip(string id);
    }
}

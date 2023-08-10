using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface IAddressService
    {
        public Task<BaseResponse<AddressDto>> CreateAddress(CreateAddressRequestModel model);
        public Task<BaseResponse<IEnumerable<AddressDto>>> GetAllAddress();
        public Task<BaseResponse<AddressDto>> GetAddress(string id);
        public Task<BaseResponse<AddressDto>> UpdateAddress(string id, UpdateAddressRequestModel model);
        public Task<BaseResponse<AddressDto>> DeleteAddress(string id);
    }
}

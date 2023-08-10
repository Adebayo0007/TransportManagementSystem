using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface IAdminService
    {
        public Task<BaseResponse<AdminDto>> CreateAdmin(CreateAdminRequestModel model);
        public Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdmins();
        public Task<BaseResponse<AdminDto>> GetAdmin(string id);
        public Task<BaseResponse<AdminDto>> UpdateAdmin(string id, UpdateAdminRequestModel model);
        public Task<BaseResponse<AdminDto>> DeleteAdmin(string id);
    }
}

using System.Linq.Expressions;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model);      
        public Task<BaseResponse<IEnumerable<UserDto>>> GetAllUsers();
        public Task<BaseResponse<UserDto>> GetUserById(string id);
        public Task<BaseResponse<UserDto>> GetUserByEmail(string email);
        public Task<BaseResponse<UserDto>> UpdateUser(string id, UpdateUserRequestModel model);
        public Task<bool> ExistById(string userId);
        public Task<bool> ExistByEmail(string userEmail);
        public Task<bool> ExistByPassword(string password);
        public Task<BaseResponse<UserDto>> DeleteUser(string id);
    }
}

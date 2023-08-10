using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Dto;

namespace TrainStationManagementApplication.Service.Interfaces
{
    public interface IRouteServicer
    {
        public Task<BaseResponse<RouteDto>> CreateRoute(string id, CreateRouteRequestModel model);
        public Task<BaseResponse<IEnumerable<RouteDto>>> GetAllRoutes();
        public Task<BaseResponse<RouteDto>> GetRoute(string id);
        public Task<BaseResponse<RouteDto>> UpdateRoute(string id, UpdateRouteRequestModel model);
        public Task<BaseResponse<RouteDto>> DeleteRoute(string id);
    }
}

using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Service.Implementation
{
    public class RouteService : IRouteServicer
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }

        public async Task<BaseResponse<RouteDto>> CreateRoute(string id, CreateRouteRequestModel model)
        {
            var routeExist = await _routeRepository.GetById(id);
            if (routeExist == null)
            {
                return new BaseResponse<RouteDto>
                {
                    Status = false,
                    Message = "Registration not successful, Route already exist",
                };
            }

            var route = new Route
            {
                StartingStation = model.StartingStation,
                EndingStation = model.EndingStation,
                Distance = model.Distance,
            };

            await _routeRepository.CreateAsync(route);

            return new BaseResponse<RouteDto>
            {
                Status = true,
                Message = "Registered successfully",
                Data = ReturnDto(route),
            };
        }

        public async Task<BaseResponse<RouteDto>> DeleteRoute(string id)
        {
            var route = await _routeRepository.GetById(id);
            if (route == null)
            {
                return new BaseResponse<RouteDto>
                {
                    Status = false,
                    Message = "Route not found",
                };
            }

            await _routeRepository.Delete(route);
            await _routeRepository.SaveChangesAsync();

            return new BaseResponse<RouteDto>
            {
                Status = true,
                Message = "Deleted successful",
            };
        }

        public async Task<BaseResponse<IEnumerable<RouteDto>>> GetAllRoutes()
        {
            var routes = await _routeRepository.GetAllAsync();
            if (routes == null)
            {
                return new BaseResponse<IEnumerable<RouteDto>>
                {
                    Status = false,
                    Message = "Routes not found",
                };
            }

            return new BaseResponse<IEnumerable<RouteDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = routes.Select(r => ReturnDto(r)).ToList(),
            };
        }

        public async Task<BaseResponse<RouteDto>> GetRoute(string id)
        {
            var route = await _routeRepository.GetById(id);
            if(route == null)
            {
                return new BaseResponse<RouteDto>
                {
                    Status = false,
                    Message = "Route not found",
                };
            }

            return new BaseResponse<RouteDto>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = ReturnDto(route),
            };
        }

        public async Task<BaseResponse<RouteDto>> UpdateRoute(string id, UpdateRouteRequestModel model)
        {
            var route = await _routeRepository.GetById(id);
            if (route == null)
            {
                return new BaseResponse<RouteDto>
                {
                    Status = false,
                    Message = "Route not found",
                };
            }
            route.StartingStation = model.StartingStation;
            route.EndingStation = model.EndingStation;
            route.Distance = model.Distance;

            _routeRepository.Update(route);
            await _routeRepository.SaveChangesAsync();

            return new BaseResponse<RouteDto>
            {
                Status = true,
                Message = "Updated successfully",
                Data = ReturnDto(route),
            };
        }

        private RouteDto ReturnDto(Route route)
        {
            return new RouteDto
            {
                Id = route.Id,
                StartingStation = route.StartingStation,
                EndingStation = route.EndingStation,
                Distance = route.Distance,
            };
        }
    }
}

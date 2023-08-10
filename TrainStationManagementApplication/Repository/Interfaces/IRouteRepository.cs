using System.Linq.Expressions;
using TrainStationManagementApplication.Models.Entities;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        public Task<bool> ExistById(string routeId);
    }
}

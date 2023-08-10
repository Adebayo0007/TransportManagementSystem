using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Repository.Implementations
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationContext _applicationContext;

        public RouteRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<Route> CreateAsync(Route route)
        {
            await _applicationContext.Routes.AddAsync(route);
            await _applicationContext.SaveChangesAsync();
            return route;
        }

        public async Task Delete(Route route)
        {
            _applicationContext.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string routeId)
        {
            return await _applicationContext
                .Routes
                .AnyAsync(u => u.Id.Equals(routeId));
        }

        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            var routes = await _applicationContext
                .Routes
                .ToListAsync();
            return routes;
        }

        public async Task<Route> GetByAny(Expression<Func<Route, bool>> expression)
        {
            var newExpression = await _applicationContext
               .Routes
               .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<Route> GetById(string id)
        {
            var route = await _applicationContext
                .Routes
                .FirstOrDefaultAsync(r => r.Id.Equals(id));
            return route;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Route Update(Route route)
        {
            _applicationContext.SaveChanges();
            return route;
        }
    }
}

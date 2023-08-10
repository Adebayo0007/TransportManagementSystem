using System.Linq.Expressions;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Repository.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        public Task<bool> ExistById(string userId);
        public Task<bool> ExistByEmail(string userEmail);
        public Task<bool> ExistByPassword(string password);

    }
}

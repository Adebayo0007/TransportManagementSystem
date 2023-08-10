using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;

namespace TrainStationManagementApplication.Repository.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationContext _applicationContext;

        public AddressRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<Address> CreateAsync(Address address)
        {
            await _applicationContext.AddAsync(address);
            await _applicationContext.SaveChangesAsync();
            return address;
        }

        public async Task Delete(Address address)
        {
             _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var addresses = await _applicationContext
                .Addresses
                .ToListAsync();
            return addresses;
        }

        public async Task<Address> GetByAny(Expression<Func<Address, bool>> expression)
        {
            var address = await _applicationContext
                .Addresses 
                .FirstOrDefaultAsync(expression);
            return address;
        }

        public async Task<Address> GetById(string id)
        {
            var address = await _applicationContext
                .Addresses
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
            return address;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Address Update(Address address)
        {
            _applicationContext.SaveChanges();
            return address;
        }
    }
}

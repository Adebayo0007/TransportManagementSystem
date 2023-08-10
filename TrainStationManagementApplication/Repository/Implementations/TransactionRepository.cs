using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;
using TrainStationManagementApplication.Data;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Repository.Interfaces;
using Transaction = TrainStationManagementApplication.Models.Entities.Transaction;
namespace TrainStationManagementApplication.Repository.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TransactionRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _applicationContext.Transactions.AddAsync(transaction);
            await _applicationContext.SaveChangesAsync();
            return transaction;
        }

        public async Task Delete(Transaction transaction)
        {
             _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var transactions = await _applicationContext
                .Transactions
                .Include(t => t.Passenger)
                .ThenInclude(t => t.User)
                .Include(t => t.Train)
                .ToListAsync();
            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsOfAPassenger(string passengerId)
        {
            var transactions = await _applicationContext
                .Transactions
                .Include(t => t.Passenger)
                .ThenInclude(t => t.User)
                .Include(t => t.Train)
                .Where(t => t.PassengerId.Equals(passengerId))
                .ToListAsync();
            return transactions;
        }

        
        public async Task<IEnumerable<Transaction>> GetAllTransactionsOfTrainForAParticularDate(string trainNumber, DateTime date)
        {
            var transactions = await _applicationContext
                .Transactions
                .Include(t => t.Passenger)
                .ThenInclude(t => t.User)
                .Include(t => t.Train)
                .Where(t => t.Train.TrainNumber.Equals(trainNumber) && t.DateCreated.Date.Equals(date.Date))
                .ToListAsync();
            return transactions;
        }

        public async Task<Transaction> GetByAny(Expression<Func<Transaction, bool>> expression)
        {
            var newExpression = await _applicationContext
                .Transactions
                .Include(t => t.Passenger)
                .ThenInclude(t => t.User)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(expression);
            return newExpression;
        }

        public async Task<Transaction> GetById(string id)
        {
            var transaction = await _applicationContext
                .Transactions
                .Include(t => t.Passenger)
                .ThenInclude(t => t.User)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(t => t.Id.Equals(id));
            return transaction;
        }

        public void SaveChanges()
        {
            _applicationContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public Transaction Update(Transaction transaction)
        {
            _applicationContext.SaveChanges();
            return transaction;
        }
    }
}

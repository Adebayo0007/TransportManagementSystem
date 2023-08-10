using System.Security.Claims;
using System.Transactions;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;
using Transaction = TrainStationManagementApplication.Models.Entities.Transaction;
namespace TrainStationManagementApplication.Service.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ITrainRepository _trainRepository;

        public TransactionService(ITransactionRepository transactionRepository, IHttpContextAccessor contextAccessor, IUserRepository userRepository, ITrainRepository trainRepository)
        {
            _transactionRepository = transactionRepository;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _trainRepository = trainRepository;
        }

        public async Task CreateTransaction(string trainId)
        {
            if(trainId != null)
            {
                var userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userRepository.GetById(userId);
                var passenger = user.Passenger;
                var train = await _trainRepository.GetById(trainId);
                var transact = new Transaction()
                {
                    Amount = train.Amount,
                    ReferenceNumber = GenerateReferenceNumber(),
                    Passenger = passenger,
                    PassengerId = passenger.Id,
                    DateCreated = DateTime.UtcNow,
                    TrainNumber = train.TrainNumber,
                    TrainId = train.Id,
                    Train = train
                };
                var transcation = await _transactionRepository.CreateAsync(transact);
                         
            }
        }

        public async Task<BaseResponse<TransactionDto>> DeleteTransaction(string id)
        {
            var transaction = await _transactionRepository.GetById(id); 
            if (transaction == null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            await _transactionRepository.Delete(transaction);
            await _transactionRepository.SaveChangesAsync();

            return new BaseResponse<TransactionDto>
            {
                Status = true,
                Message = "Deleted successful",
            };

        }

        public async Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            if (transactions == null)
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = transactions.Select(t => ReturnTransactionDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactionsOfAPassenger(string passengerId)
        {
            var transactions = await _transactionRepository.GetAllTransactionsOfAPassenger(passengerId);
            if (transactions == null)
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = transactions.Select(t => ReturnTransactionDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllTransactionsOfTrainForAParticularDate(string trainNumber, DateTime date)
        {
            var transactions = await _transactionRepository.GetAllTransactionsOfTrainForAParticularDate(trainNumber, date);
            if (transactions == null)
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = transactions.Select(t => ReturnTransactionDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<TransactionDto>> GetTransaction(string id)
        {
            var transaction = await _transactionRepository.GetById(id);
            if (transaction == null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            return new BaseResponse<TransactionDto>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = ReturnTransactionDto(transaction),
            };
        }

        public async Task<BaseResponse<TransactionDto>> UpdateTransaction(string id, UpdateTransactionRequestModel model)
        {
            var transaction = await _transactionRepository.GetById(id);
            if (transaction == null)
            {
                return new BaseResponse<TransactionDto>
                {
                    Status = false,
                    Message = "Transaction not found",
                };
            }

            transaction.Amount = model.Amount;
            transaction.Passenger.User.FirstName = model.FirstName;
            transaction.Passenger.User.LastName = model.LastName;
            transaction.Passenger.User.PhoneNumber = model.PhoneNumber;
            transaction.Passenger.User.Gender = model.Gender;
            transaction.DateCreated = model.DateCreated;

            _transactionRepository.Update(transaction);
            await _transactionRepository.SaveChangesAsync();

            return new BaseResponse<TransactionDto>
            {
                Status = true,
                Message = "Updated successful",
                Data = ReturnTransactionDto(transaction),
            };
        }

        private TransactionDto ReturnTransactionDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                FirstName = transaction.Passenger.User.FirstName,
                LastName = transaction.Passenger.User.LastName,
                Email = transaction.Passenger.User.Email,
                PhoneNumber = transaction.Passenger.User.PhoneNumber,
                Gender = transaction.Passenger.User.Gender,
                DateCreated = transaction.DateCreated,
                ReferenceNumber = transaction.ReferenceNumber,
            };
        }

        private string GenerateReferenceNumber()
        {
            return $"TSM/{Guid.NewGuid().ToString().Substring(0,10)}";
        }
    } 
}

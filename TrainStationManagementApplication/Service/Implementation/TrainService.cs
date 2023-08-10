using System.Diagnostics;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;
using Route = TrainStationManagementApplication.Models.Entities.Route;
namespace TrainStationManagementApplication.Service.Implementation
{
    public class TrainService : ITrainService
    {
        private readonly ITrainRepository _trainRepository;
        private readonly IRouteRepository _routeRepository;

        public TrainService(ITrainRepository trainRepository, IRouteRepository routeRepository, IUserRepository userRepository, IPassengerRepository passengerRepository)
        {
            _trainRepository = trainRepository ?? throw new ArgumentNullException(nameof(trainRepository));
            _routeRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }

        public async Task<BaseResponse<TrainDto>> CreateTrain(CreateTrainRequestModel model)
        {
            var trainExist = await _trainRepository.GetByAny(t => t.TrainNumber.Equals(model.TrainNumber));
            if (trainExist != null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Registration not successful, Train already exist",
                };
            }

            var route = new Route
            {
                StartingStation = model.StartingStation,
                EndingStation = model.EndingStation,
                Distance = model.Distance,
            };

            var train = new Train
            {
                Route = route,
                Name = model.Name,
                TrainNumber = model.TrainNumber,
                DepartureTime = model.DepartureTime,
                AvailableSpace = model.AvailableSpace,
                Amount = model.Amount,
                Capacity = model.Capacity,
                IsAvailable = model.IsAvailable,
                SeatTrack = 0
            };

            await _trainRepository.CreateAsync(train);

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Registered successfully",
                Data = ReturnDto(train),
            };
        }

        public async Task<BaseResponse<TrainDto>> GetTrainByNumber(string trainNumber)
        {
            var train = await _trainRepository.GetByAny(t => t.TrainNumber.Equals(trainNumber));
            if (train == null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Not found",
                };
            }

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = ReturnDto(train),
            };
        }

        public async Task<BaseResponse<TrainDto>> DeleteTrain(string id)
        {
            var train = await _trainRepository.GetById(id);
            if (train == null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not found",
                };
            }

            train.IsDeleted = true;
            await _trainRepository.SaveChangesAsync();

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Deleted successfully",
                Data = ReturnDto(train),
            };
        }

        public async Task<BaseResponse<IEnumerable<TrainDto>>> GetAllTrain()
        {
            var trains = await _trainRepository.GetAllAsync();
            if (trains == null)
            {
                return new BaseResponse<IEnumerable<TrainDto>>
                {
                    Status = false,
                    Message = "Trains not found",
                };
            }

            return new BaseResponse<IEnumerable<TrainDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = trains.Select(t => ReturnDto(t)).ToList()
            };
        }


        public async Task<BaseResponse<IEnumerable<TrainDto>>> GetAvailableTrains()
        {
            var trains = await _trainRepository.GetAvailableTrains();
            if (trains == null)
            {
                return new BaseResponse<IEnumerable<TrainDto>>
                {
                    Status = false,
                    Message = "Trains not found",
                };
            }

            return new BaseResponse<IEnumerable<TrainDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = trains.Select(t => ReturnDto(t)).ToList()
            };
        }
        public async Task<BaseResponse<IEnumerable<TrainDto>>> GetUnAvailableTrains()
        {
             var trains = await _trainRepository.GetUnAvailableTrains();
            if (trains == null)
            {
                return new BaseResponse<IEnumerable<TrainDto>>
                {
                    Status = false,
                    Message = "Trains not found",
                };
            }

            return new BaseResponse<IEnumerable<TrainDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = trains.Select(t => ReturnDto(t)).ToList()
            };

        }

        public async Task<BaseResponse<TrainDto>> GetTrain(string id)
        {
            var train = await _trainRepository.GetById(id);
            if (train == null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not found",
                };
            }

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnDto(train),
            };
        }

        public async Task<BaseResponse<TrainDto>> GetTrainAfterBeingUnAvailable(string trainId)
        {
            var train = await _trainRepository.GetTrainAfterBeingNonAvailable(trainId);
            if (train == null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not found",
                };
            }

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnDto(train),
            };

        }

        public async Task<BaseResponse<IEnumerable<TrainDto>>> GetTrainsByName(string name)
        {
           if(name.Equals(null))
            {
                return new BaseResponse<IEnumerable<TrainDto>>()
                {
                    Status = false,
                    Message = "invalid input"
                };
            }
           var trains = await _trainRepository.GetTrainsByName(name);
            var trainDto =  trains.Select(t => new TrainDto
            {
                Name = t.Name,
                TrainNumber = t.TrainNumber,
                DepartureTime = t.DepartureTime,
                AvailableSpace = t.AvailableSpace,
                Capacity = t.Capacity,

            }).AsEnumerable();
            return new BaseResponse<IEnumerable<TrainDto>>()
            {
                Status = true,
                Message = "Trains retrieved successfully",
                Data = trainDto
            };
        }

        public async Task<BaseResponse<TrainDto>> UpdateTrain(string id, UpdateTrainRequestModel model)
        {
            var train = await _trainRepository.GetById(id);
            if (train == null)
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not found",
                };
            }

            train.Name = model.Name;
            return new BaseResponse<TrainDto> 
            {
                Status = true, 
                Message = "Updated successful",
                Data = ReturnDto(train),
            };
        }
        public async Task UpdateTrainBackToAvailable(string trainId)
        {
            var train = await _trainRepository.GetByIdToUpdateBackToAvailable(trainId);
            train.AvailableSpace = train.Capacity;
            train.IsAvailable = true;
            _trainRepository.Update(train);
        }

        public async Task<BaseResponse<TrainDto>> UpdateTrainToIsAvailable(string trainNumber)
        {
            if(trainNumber.Equals(null))
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train number not available"
                };
            }
            var train = await _trainRepository.GetByAny(t => t.TrainNumber.Equals(trainNumber));
            if (train.Equals(null))
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not available"
                };
            }
            train.IsAvailable = true;
            train.AvailableSpace = 0;
            _trainRepository.Update(train);

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Train updated successfully",
                Data = ReturnDto(train),
            };

        }

        public async Task<BaseResponse<TrainDto>> UpdateTrainToNotAvailable(string trainNumber)
        {
            if (trainNumber.Equals(null))
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train number not available"
                };
            }

            var train = await _trainRepository.GetByAny(t => t.TrainNumber.Equals(trainNumber));

            if (train.Equals(null))
            {
                return new BaseResponse<TrainDto>
                {
                    Status = false,
                    Message = "Train not available"
                };
            }

            train.IsAvailable = false;
            _trainRepository.Update(train);         

            return new BaseResponse<TrainDto>
            {
                Status = true,
                Message = "Train updated successfully",
                Data = ReturnDto(train),
            };
        }

        private TrainDto ReturnDto(Train train)
        {
            return new TrainDto
            {
                Id = train.Id,
                Name = train.Name,
                TrainNumber = train.TrainNumber,
                DepartureTime = train.DepartureTime,
                AvailableSpace = train.AvailableSpace,
                Amount = train.Amount,
                Capacity = train.Capacity,
                IsAvailable = train.IsAvailable,
                StartingStation = train.Route.StartingStation,
                EndingStation = train.Route.EndingStation,
                Distance = train.Route.Distance,
                DateCreated = train.DateCreated,               
            };
        }

    }
}

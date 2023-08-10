using System.Reflection.Metadata;
using System.Security.Claims;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.EmailServices.Interfaces;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Models.Enums;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Service.Implementation
{
    public class TripService : ITripService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITripRepository _tripRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITrainRepository _trainRepository;
        private readonly ITrainService _trainService;
        private readonly IMailService _mailService;
        public TripService(ITripRepository tripRepository,
            IHttpContextAccessor contextAccessor, 
            IPassengerRepository passengerRepository, 
            IUserRepository userRepository, 
            ITrainRepository trainRepository,
            ITrainService trainService,
            IMailService mailService)
        {
            _contextAccessor = contextAccessor;
            _tripRepository = tripRepository;
            _passengerRepository = passengerRepository;
            _userRepository = userRepository;
            _trainRepository = trainRepository;
            _trainService = trainService;
            _mailService = mailService;

        }
        public async Task<BaseResponse<TripDto>> CreateTrip(CreateTripRequestModel model)
        {
            var passengerId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //passengerId = "ff558952-f";
            var passenger = await _passengerRepository.GetById(passengerId);
            var userExist = await _userRepository.ExistById(passenger.UserId);
            if (!userExist)
            {
                return new BaseResponse<TripDto>()
                {
                    Status = false,
                    Message = "Passenger do not exist",
                };
            }
                var train = await _trainRepository.GetById(model.TrainId);
                
                if (train == null) 
                {
                    return new BaseResponse<TripDto>()
                    {
                        Status = false,
                        Message = "Train does not exist",
                    };
                }
                if (train.AvailableSpace == 0)
                {
                    return new BaseResponse<TripDto>()
                    {
                        Status = false,
                        Message = "No seat available",
                    };
                }
                var trip = new Trip()
                {
                    Name = train.Name,
                    Destination = model.Destination,
                    StartingStation = train.Route.StartingStation,
                    EndingStation = train.Route.EndingStation,
                    DepartureTime = train.DepartureTime,
                    SeatNumber = train.SeatTrack == 0? 1: train.SeatTrack+1,
                    Distance = train.Route.Distance,
                    PassengerId = passenger.Id,
                    Passenger = passenger,
                    TrainId = train.Id,
                    TrainNumber = train.TrainNumber,
                    Train = train,
                    IsDeleted = false,
                };
                  train.AvailableSpace -= 1;
                  train.SeatTrack = trip.SeatNumber;

            if (train.AvailableSpace == 0)
            {
                //Sending mail to passengers about the time.
                var passengersNeededTobeSentMail = await _tripRepository.GetAllTripsOfToSendEmail(train.TrainNumber);
                
                train.SeatTrack = 0;
                    
                await _trainService.UpdateTrainToNotAvailable(train.TrainNumber);
                var path = File.ReadAllText("C:\\Users\\hp\\source\\repos\\TrainStationManagementApplication\\TrainStationManagementApplication\\Views\\Email\\Email.cshtml") ;
                foreach (var pass in passengersNeededTobeSentMail)
                {
                    var mail = new CreateMailRequestModel()
                    {
                        HtmlContent = path.Replace("{{passenger}}", pass.Passenger.User.FirstName),
                        ToEmail = pass.Passenger.User.Email,
                        ToName = pass.Passenger.User.FirstName,
                        Subject = "Train availability",
                    };
                    await _mailService.SendEMailAsync(mail);

                }
            }

                 await _tripRepository.CreateAsync(trip);
               
                return new BaseResponse<TripDto>()
                {
                    Status = true,
                    Message = "Trip registered successfully",
                    Data = ReturnTripDto(trip)

                };           
        }

        public async Task<BaseResponse<TripDto>> DeleteTrip(string id)
        {
            var trip = await _tripRepository.GetById(id);
            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "Trip not found",
                };
            }

            trip.IsDeleted = true;
            await _tripRepository.SaveChangesAsync();

            return new BaseResponse<TripDto>
            {
                Status = true,
                Message = "Deleted successfully",
            };
        }

        public async Task<byte[]> GenerateReceipt(string passengerName, decimal amountPaid, DateTime paymentDate)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document(); // Update the namespace here
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();

                // Add content to the PDF
                Paragraph header = new Paragraph("Receipt");
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                Paragraph passengerInfo = new Paragraph($"Passenger Name: {passengerName}");
                document.Add(passengerInfo);

                Paragraph amountInfo = new Paragraph($"Amount Paid: {amountPaid:C}");
                document.Add(amountInfo);

                Paragraph dateInfo = new Paragraph($"Payment Date: {paymentDate.ToShortDateString()}");
                document.Add(dateInfo);

                document.Close();

                return ms.ToArray();
            }
        }

        public async Task<BaseResponse<IEnumerable<TripDto>>> GetAllTrips()
        {
            var trips = await _tripRepository.GetAllAsync();
            if (trips == null)
            {
                return new BaseResponse<IEnumerable<TripDto>>
                {
                    Status = false,
                    Message = "Trips not found",
                };
            }

            return new BaseResponse<IEnumerable<TripDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = trips.Select(t => ReturnTripDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfATrain(string trainNumber)
        {
            var trips = await _tripRepository.GetAllTripsOfATrain(trainNumber);
            if (trips == null)
            {
                return new BaseResponse<IEnumerable<TripDto>>
                {
                    Status = false,
                    Message = "Trip not found",
                };
            }

            return new BaseResponse<IEnumerable<TripDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = trips.Select(t => ReturnTripDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfToday()
        {
            var trips = await _tripRepository.GetAllTripsOfToday();
            if (trips == null)
            {
                return new BaseResponse<IEnumerable<TripDto>>
                {
                    Status = false,
                    Message = "Trip not found",
                };
            }

            return new BaseResponse<IEnumerable<TripDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = trips.Select(t => ReturnTripDto(t))
                .ToList(),
            };
        }

        public async Task<BaseResponse<IEnumerable<TripDto>>> GetAllTripsOfTrainForParticularDay(DateTime date, string trainNumber)
        {
            var trips = await _tripRepository.GetAllTripsOfTrainForParticularDay(date, trainNumber);
            if (trips == null)
            {
                return new BaseResponse<IEnumerable<TripDto>>
                {
                     Status = false,
                     Message = "Trips not found",
                };
            }

            return new BaseResponse<IEnumerable<TripDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = trips.Select(t => ReturnTripDto(t)).ToList(),
            };
        }

        public async Task<BaseResponse<TripDto>> GetTrip(string id)
        {
            var trip = await _tripRepository.GetById(id);
            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "Trip not found",
                };
            }

            return new BaseResponse<TripDto>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = ReturnTripDto(trip),
            };
        }

        public async Task<BaseResponse<TripDto>> UpdateTrip(string id, UpdateTripRequestModel model)
        {
            var trip = await _tripRepository.GetById(id);
            if (trip == null)
            {
                return new BaseResponse<TripDto>
                {
                    Status = false,
                    Message = "Trip not found",
                };
            }

            trip.Name = model.TripName;
            trip.Destination = model.Destination;
            trip.StartingStation = model.StartingStation;
            trip.EndingStation = model.EndingStation;
            trip.DepartureTime = model.DepartureTime;
            trip.Distance = model.Distance;
            trip.SeatNumber = model.SeatNumber;

            trip.IsDeleted = false;
            _tripRepository.Update(trip);
            await _tripRepository.SaveChangesAsync();

            return new BaseResponse<TripDto>
            {
                Status = true,
                Message = "Updated successful",
                Data = ReturnTripDto(trip),               
            };
        }

        private TripDto ReturnTripDto(Trip trip)
        {
            return new TripDto
            {
                Id = trip.Id,
                TripName = trip.Name,
                DepartureTime = trip.DepartureTime,
                Destination = trip.Destination,
                Distance = trip.Distance,
                StartingStation = trip.StartingStation,
                EndingStation = trip.EndingStation,
                TrainName = trip.Name,
                TrainNumber = trip.TrainNumber,
                AvailableSpace = trip.Train.AvailableSpace,
                Capacity = trip.Train.Capacity,
                SeatNumber = trip.SeatNumber,
                IsAvailable = trip.Train.IsAvailable,
            };
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Service.Implementation
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PassengerService(IPassengerRepository passengerRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _passengerRepository = passengerRepository ?? throw new ArgumentNullException(nameof(passengerRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public async Task<BaseResponse<PassengerDto>> CreatePassenger(CreatePassengerRequestModel model)
        {
           var userExist = await _userRepository.ExistByEmail(model.Email);
            if(userExist) 
            {
                return new BaseResponse<PassengerDto>
                {
                    Status = false,
                    Message = "User already Exist",
                };
            }

            var imageValidate = ImageValidator(model.Image);

            if (!imageValidate.Status)
            {
                return new BaseResponse<PassengerDto>
                {
                    Status = false,
                    Message = imageValidate.Message,
                };
            }

            var address = new Address
            {
                Street = model.Street,
                City = model.City,
                State = model.State,
                Country = model.Country,
            };
            var user = new User
            {
                Address = address,
                Role = "Passenger",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Image = FileUpload(model.Image),
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
            };
            //var verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            var passenger = new Passenger
            {
                User = user,
                UserId = user.Id,
            };

            await _userRepository.CreateAsync(user);
            await _passengerRepository.CreateAsync(passenger);
            await _passengerRepository.SaveChangesAsync();
          
            return new BaseResponse<PassengerDto>
            {
                Status = true,
                Message = "Created successfully",
                Data = ReturnDto(passenger),
            };
        }

        public async Task<BaseResponse<PassengerDto>> DeletePassenger(string id)
        {
            var passenger = await _passengerRepository.GetById(id);
            if (passenger == null)
            {
                return new BaseResponse<PassengerDto>
                {
                    Status = false,
                    Message = "Passenger not found",
                };
            }

            passenger.User.IsDeleted = true;
            await _passengerRepository.SaveChangesAsync();

            return new BaseResponse<PassengerDto> 
            {
                Status = true,
                Message = "Deleted successfully",
            };
        }

        public async Task<BaseResponse<IEnumerable<PassengerDto>>> GetAllPassengers()
        {
            var passengers = await _passengerRepository.GetAllAsync();
            //var passengers = pass.Where(x => x.User.IsDeleted);
            if (passengers == null)
            {
                return new BaseResponse<IEnumerable<PassengerDto>>
                {
                    Status = false,
                    Message = "Passengers not found",
                };
            }

            return new BaseResponse<IEnumerable<PassengerDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = passengers.Select(p => ReturnDto(p)).ToList(),
            };
        }

        public async Task<BaseResponse<PassengerDto>> GetPassenger(string id)
        {
            var passenger = await _passengerRepository.GetById(id);
            if (passenger == null)
            {
                return new BaseResponse<PassengerDto>
                {
                    Status = false,
                    Message = "Passenger not found",
                };
            }

            return new BaseResponse<PassengerDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnDto(passenger),
            };
        }

        public async Task<BaseResponse<PassengerDto>> UpdatePassenger(string id, UpdatePassengerRequestModel model)
        {
            var passenger = await _passengerRepository.GetById(id);
            if (passenger == null)
            {
                return new BaseResponse<PassengerDto>
                {
                    Status = false,
                    Message = "Passenger not found",
                };
            }

            passenger.User.FirstName = model.FirstName ?? passenger.User.FirstName;
            passenger.User.LastName = model.LastName ?? passenger.User.LastName;
            passenger.User.Image = model.Image != null ? FileUpload(model.Image) : passenger.User.Image;
            passenger.User.Gender = model.Gender != passenger.User.Gender ? model.Gender: passenger.User.Gender;
            passenger.User.PhoneNumber = model.PhoneNumber ?? passenger.User.PhoneNumber;
            passenger.User.Address.State = model.State ?? passenger.User.Address.State;
            passenger.User.Address.Street = model.Street ?? passenger.User.Address.Street;
            passenger.User.Address.City = model.City ?? passenger.User.Address.City;
            passenger.User.Address.Country = model.Country ?? passenger.User.Address.Country;

            passenger.User.IsDeleted = false;
            _passengerRepository.Update(passenger);
            await _passengerRepository.SaveChangesAsync();

            return new BaseResponse<PassengerDto>
            {
                Status = true,
                Message = "Updated successfully",
                Data = ReturnDto(passenger),
            };
        }


        private string FileUpload(IFormFile file)
        {
            var webImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "upload/image");

            if (!Directory.Exists(webImagePath))
            {
                Directory.CreateDirectory(webImagePath);
            }

            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            var filePath = Path.Combine(webImagePath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName;
        }


        private BaseResponse<PassengerDto> ImageValidator(IFormFile file)
        {

            if (file.FileName is null || file.FileName.Length <= 0)
            {
                return new BaseResponse<PassengerDto>()
                {
                    Status = false,
                    Message = "Please select a profile picture"
                };
            }

            if (file.Length > 100000000)
            {
                return new BaseResponse<PassengerDto>()
                {
                    Status = false,
                    Message = "File size cannot me more than 64kb"
                };
            }

            var acceptableExtension = new List<string>() { ".jpg", ".jpeg", ".png", ".dnb" };

            var fileExtension = Path.GetExtension(file.FileName);

            if (!acceptableExtension.Contains(fileExtension.ToLower()))
            {
                return new BaseResponse<PassengerDto>()
                {
                    Status = false,
                    Message = "File format not supported, please upload a picture"
                };
            }

            return new BaseResponse<PassengerDto>()
            {
                Status = true,
                Message = "Success"
            };
        }


        private PassengerDto ReturnDto(Passenger passenger)
        {
            return new PassengerDto()
            {
                Id = passenger.Id,
                FirstName = passenger.User.FirstName,
                LastName = passenger.User.LastName,
                Email = passenger.User.Email,
                Image = passenger.User.Image,
                Gender = passenger.User.Gender,
                PhoneNumber = passenger.User.PhoneNumber,
                Street = passenger.User.Address.Street,
                State = passenger.User.Address.State,
                City = passenger.User.Address.City,
                Country = passenger.User.Address.Country,
            };
        }
    }
}

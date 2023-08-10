using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponse<UserDto>> DeleteUser(string id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = "User not found",
                };
            }

            user.IsDeleted = true;
            await _userRepository.SaveChangesAsync();

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Deleted successfully",
            };
        }

        public async Task<bool> ExistByEmail(string userEmail)
        {
            var user = await _userRepository.ExistByEmail(userEmail);
            return user;
        }

        public async Task<bool> ExistById(string userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return false;
            }
            await _userRepository.ExistById(userId);
            return true;
        }

        public async Task<bool> ExistByPassword(string password)
        {
            var user = await _userRepository.ExistByPassword(password);
            return user;
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            var user = await _userRepository.GetAllAsync();
            if (user == null)
            {
                return new BaseResponse<IEnumerable<UserDto>>
                { 
                    Status = false,
                    Message = "User not found",
                };
            }
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = user.Select(u => ReturnUserDto(u))
                .ToList(),
            };
        }


        public async Task<BaseResponse<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByAny(u => u.Email == email);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = "user not found",
                };
            }

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnUserDto(user),
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserById(string id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = "User not found",
                };
            }

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnUserDto(user),
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetByAny(u => u.Email == model.Email && u.IsDeleted == false);
            var verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (user == null || !verified)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = "Invalid Login Credentials",
                };
            }

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Login Successful",
                Data = ReturnUserDto(user)
            };
        }


        public async Task<BaseResponse<UserDto>> UpdateUser(string id, UpdateUserRequestModel model)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = "User not found",
                };
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Image = model.Image;
            user.Role = model.Role;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;
            user.Address.Street = model.Street;
            user.Address.State = model.State;
            user.Address.City = model.City;
            user.Address.Country = model.Country;

            user.IsDeleted = false;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "Updated successfully",
                Data = ReturnUserDto(user),
            };
        }



        public UserDto ReturnUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Image = user.Image,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Street = user.Address.Street,
                State = user.Address.State,
                City = user.Address.City,
                Country = user.Address.Country,
            };
        }
    }
}

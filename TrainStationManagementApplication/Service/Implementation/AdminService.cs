using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Service.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public async Task<BaseResponse<AdminDto>> CreateAdmin(CreateAdminRequestModel model)
        {
            var adminExist = await _userRepository.ExistByEmail(model.Email);
            if (adminExist)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "Registration not successful, Admin already exist",
                };
            }

            var imageValidate = ImageValidator(model.Image);

            if (!imageValidate.Status)
            {
                return new BaseResponse<AdminDto>
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
                Role = "Admin",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Image = FileUpload(model.Image),
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
            };
            var verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            var admin = new Admin
            {
                User = user,
                UserId = user.Id,
                StaffNumber = GenerateStaffNumber(),
            };

            await _userRepository.CreateAsync(user);
            await _adminRepository.CreateAsync(admin);

            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = "Registered successfully",
                Data = ReturnDto(admin),              
            };
        }

        public async Task<BaseResponse<AdminDto>> DeleteAdmin(string id)
        {
            var admin = await _adminRepository.GetById(id);
            if (admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "Admin not found",
                };
            }

            admin.User.IsDeleted = true;
            await _adminRepository.SaveChangesAsync();

            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = "Deleted successful",
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdmin(string id)
        {
            var admin = await _adminRepository.GetById(id);
            if (admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "Admin not found",
                };
            }

            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = ReturnDto(admin),
            };
        }

        public async Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAsync();
            if (admins == null)
            {
                return new BaseResponse<IEnumerable<AdminDto>>
                {
                    Status = false,
                    Message = "Admins not found",
                };
            }

            return new BaseResponse<IEnumerable<AdminDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = admins.Select(a => ReturnDto(a)).ToList(),
            };
        }

        public async Task<BaseResponse<AdminDto>> UpdateAdmin(string id, UpdateAdminRequestModel model)
        {
            var admin = await _adminRepository.GetById(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "Admin not found",
                };
            }

            admin.User.FirstName = model.FirstName;
            admin.User.LastName = model.LastName;
            admin.User.PhoneNumber = model.PhoneNumber;
            admin.User.Gender = model.Gender;
            admin.User.Image = FileUpload(model.Image);
            admin.User.Address.Street = model.Street;
            admin.User.Address.State = model.State;
            admin.User.Address.City = model.City;
            admin.User.Address.Country = model.Country;

            admin.User.IsDeleted = false;
            _adminRepository.Update(admin);
            await _adminRepository.SaveChangesAsync();

            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = "Deleted successfully",
                Data = ReturnDto(admin),
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


        private BaseResponse<AdminDto> ImageValidator(IFormFile file)
        {

            if (file.FileName is null || file.FileName.Length <= 0)
            {
                return new BaseResponse<AdminDto>()
                {
                    Status = false,
                    Message = "Please select a profile picture"
                };
            }

            if (file.Length > 100000000)
            {
                return new BaseResponse<AdminDto>()
                {
                    Status = false,
                    Message = "File size cannot me more than 64kb"
                };
            }

            var acceptableExtension = new List<string>() { ".jpg", ".jpeg", ".png", ".dnb" };

            var fileExtension = Path.GetExtension(file.FileName);

            if (!acceptableExtension.Contains(fileExtension.ToLower()))
            {
                return new BaseResponse<AdminDto>()
                {
                    Status = false,
                    Message = "File format not supported, please upload a picture"
                };
            }

            return new BaseResponse<AdminDto>()
            {
                Status = true,
                Message = "Success",
            };
        }


        private AdminDto ReturnDto(Admin admin)
        {
            return new AdminDto()
            {
                Id = admin.Id,
                FirstName = admin.User.FirstName,
                LastName = admin.User.LastName,
                Email = admin.User.Email,
                Image = admin.User.Image,
                Gender = admin.User.Gender,
                PhoneNumber = admin.User.PhoneNumber,
                Street = admin.User.Address.Street,
                State = admin.User.Address.State,
                City = admin.User.Address.City,
                Country = admin.User.Address.Country,
            };
        }

        private string GenerateStaffNumber()
        {
            return "STF" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}

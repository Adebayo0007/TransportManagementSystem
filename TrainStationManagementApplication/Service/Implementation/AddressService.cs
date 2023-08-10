using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.Dto.ResponseModel;
using TrainStationManagementApplication.Models.Entities;
using TrainStationManagementApplication.Repository.Implementations;
using TrainStationManagementApplication.Repository.Interfaces;
using TrainStationManagementApplication.Service.Interfaces;

namespace TrainStationManagementApplication.Service.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<BaseResponse<AddressDto>> CreateAddress(CreateAddressRequestModel model)
        {
            var addressExist = await _addressRepository.GetByAny(a => a.City == model.City);
            if (addressExist == null)
            {
                return new BaseResponse<AddressDto>
                {
                    Status = false,
                    Message = "Registration not successful, Address already exist",
                };
            }

            var address = new Address
            {
                Country = model.Country,
                State = model.State,
                Street = model.Street,
                City = model.City,
            };

            await _addressRepository.CreateAsync(address);

            return new BaseResponse<AddressDto>
            {
                Status = true,
                Message = "Registered successfully",
                Data = ReturnAddressDto(address),
            };
        }

        public async Task<BaseResponse<AddressDto>> DeleteAddress(string id)
        {
            var address = await _addressRepository.GetById(id);
            if (address == null)
            {
                return new BaseResponse<AddressDto>
                {
                    Status = false,
                    Message = "Address not found",
                };
            }

           await _addressRepository.Delete(address);
           await _addressRepository.SaveChangesAsync();

            return new BaseResponse<AddressDto>
            {
                Status = true,
                Message = "Deleted successfully",
            };
        }

        public async Task<BaseResponse<AddressDto>> GetAddress(string id)
        {
            var address = await _addressRepository.GetById(id);
            if(address == null)
            {
                return new BaseResponse<AddressDto>
                {
                    Status = false,
                    Message = "Address not found",
                };
            }

            return new BaseResponse<AddressDto>
            {
                Status = true,
                Message = "Retrieved successfully",
                Data = ReturnAddressDto(address),
            };
        }

        public async Task<BaseResponse<IEnumerable<AddressDto>>> GetAllAddress()
        {
            var addresses = await _addressRepository.GetAllAsync();
            if (addresses == null)
            {
                return new BaseResponse<IEnumerable<AddressDto>>
                {
                    Status = false,
                    Message = "Addresses not Found",
                };
            }

            return new BaseResponse<IEnumerable<AddressDto>>
            {
                Status = true,
                Message = "Retrieved successful",
                Data = addresses.Select(x => ReturnAddressDto(x)).ToList(),
            };
        }

        public async Task<BaseResponse<AddressDto>> UpdateAddress(string id, UpdateAddressRequestModel model)
        {
            var address = await _addressRepository.GetById(id);
            if (address == null)
            {
                return new BaseResponse<AddressDto>
                {
                    Status = false,
                    Message = "Address not found",
                };
            }

            address.Street = model.Street;
            address.City = model.City;
            address.State = model.State;
            address.Country = model.Country;

            _addressRepository.Update(address);
            await _addressRepository.SaveChangesAsync();

            return new BaseResponse<AddressDto>
            {
                Status = true,
                Message = "Updated successfully",
                Data = ReturnAddressDto(address),
            };
        }

        private AddressDto ReturnAddressDto(Address address)
        {
            return new AddressDto
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                Country = address.Country,
            };
        }
    }
}
